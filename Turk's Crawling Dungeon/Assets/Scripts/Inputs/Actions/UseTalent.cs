using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;
using TCD.Indicators;
using TCD.Texts;

namespace TCD.Inputs.Actions
{
    public class UseTalent : PlayerAction
    {
        public Talent talent;

        public override string Name => "Use Talent";

        public override MovementMode MovementMode => GetMovementMode();

        public UseTalent(Talent talent) : base()
        {
            this.talent = talent;
        }

        public override void Start()
        {
            base.Start();
            if (!string.IsNullOrEmpty(talent.Indicator))
                IndicatorHandler.ShowIndicator(talent.Indicator, talent.GetRange(), true);
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            if (talent.UseMode == UseMode.Toggle)
            {
                if (talent.isActive)
                    talent.Deactivate();
                else
                    talent.Activate();
                playerActionManager.CancelCurrentAction();
            }
            if (talent.UseMode == UseMode.Passive)
                playerActionManager.CancelCurrentAction();

        }

        public override void End()
        {
            base.End();
            IndicatorHandler.HideIndicator();
        }

        private MovementMode GetMovementMode()
        {
            switch (talent.TargetMode)
            {
                case TargetMode.Attack:
                    return MovementMode.Point;
                default:
                    return MovementMode.Cursor;
            }
        }

        public override int GetRange() => talent.GetRange();

        public override IEnumerator OnObject(BaseObject target)
        {
            yield return talent.OnObjectRoutine(target);
        }


        public override IEnumerator OnCell(Cell cell)
        {
            BaseObject target = null;
            foreach (BaseObject obj in cell.objects)
            {
                if (obj.parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer() &&
                    (!target || obj.parts.Get<Combat>()))
                    target = obj;
            }
            if (talent.TargetMode == TargetMode.Object)
            {
                if (!target)
                {
                    FloatingTextHandler.Draw(PlayerInfo.currentPlayer.transform.position, "Must target an object!", Color.red);
                    yield break;
                }
                yield return OnObject(target);
            }
            yield return talent.OnCellRoutine(cell);
        }
    }
}
