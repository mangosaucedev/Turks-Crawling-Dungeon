using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;
using TCD.Graphics.Indicators;
using TCD.Texts;
using TCD.TimeManagement;

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
                IndicatorHandler.ShowIndicator(talent.Indicator, talent.GetRange(talent.level), true);
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            if (talent.UseMode == UseMode.Passive)
            {
                playerActionManager.CancelCurrentAction();
                return;
            }
            if (talent.TargetMode == TargetMode.None)
            {
                talent.Activate();
                playerActionManager.CancelCurrentAction();
                EventManager.Send(new TalentUsedEvent(talent));
            }
            if (talent.UseMode == UseMode.Toggle)
            {
                if (talent.isActive)
                    talent.Deactivate();
                else
                    talent.Activate();
                playerActionManager.CancelCurrentAction();
                EventManager.Send(new TalentUsedEvent(talent));
            }
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

        public override int GetRange() => talent.GetRange(talent.level);

        public override IEnumerator OnObject(BaseObject target)
        {
            if (talent.OnObject(target))
                TimeScheduler.Tick(talent.GetEnergyCost());
            yield break;
        }


        public override IEnumerator OnCell(Cell cell)
        {
            BaseObject target = null;
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj.Parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer() &&
                    (!target || obj.Parts.Get<Combat>()))
                    target = obj;
            }
            if (talent.TargetMode != TargetMode.Cell)
            {
                if (!target)
                {
                    FloatingTextHandler.Draw(PlayerInfo.currentPlayer.transform.position, "Must target an object!", Color.red);
                    yield break;
                }
                yield return OnObject(target);
            }
            if (talent.OnCell(cell))
                TimeScheduler.Tick(talent.GetEnergyCost());
        }
    }
}
