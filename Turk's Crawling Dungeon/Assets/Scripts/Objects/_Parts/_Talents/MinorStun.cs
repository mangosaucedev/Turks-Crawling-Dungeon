using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Objects.Parts.Effects;
using TCD.Texts;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public class MinorStun : Talent
    {
        public override string Name => "Minor Stun";

        public override string TalentTree => "RandomPsionics";

        public override Sprite Icon => Assets.Get<Sprite>("PsiStunIcon");

        public override Resource Resource => Resource.Psi;

        public override string Indicator => "Player To Cursor Indicator";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Object;

        public override int GetActivationResourceCost()
        {
            switch (level)
            {
                default:
                    return 8;
                case 2:
                    return 10;
                case 3:
                    return 11;
                case 4:
                    return 13;
                case 5:
                    return 14;
            }
        }

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 14 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 12 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 10 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 9 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange()
        {
            switch (level)
            {
                default:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 6;
                case 4:
                    return 6;
                case 5:
                    return 7;
            }
        }

        public int GetDuration()
        {
            switch (level)
            {
                default:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription() => $"Conjure a psychic blast that stuns the " +
            $"target for {((float) GetDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} turns.";

        protected override bool OnAIBeforeMove(AIBeforeMoveEvent e)
        {
            int distanceToTarget = Mathf.FloorToInt(Vector2Int.Distance(Position, e.targetPosition));
            if (CanUseTalent() && distanceToTarget <= GetRange() && !e.hasActed)
            {

            }
            return base.OnAIBeforeMove(e);
        }
    }
}
