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
    [PlayerTalent("MinorFireblast"), Serializable]
    public class MinorFireblast : Talent
    {
        public override string Name => "Minor Fireblast";

        public override string TalentTree => "RandomPsionics";

        public override string IconName => "FireblastIcon";

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
                    return 14;
                case 2:
                    return 16;
                case 3:
                    return 19;
                case 4:
                    return 22;
                case 5:
                    return 24;
            }
        }

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 12 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 11 * TimeInfo.TIME_PER_STANDARD_TURN;
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
                    return 6;
                case 2:
                    return 8;
                case 3:
                    return 10;
                case 4:
                    return 11;
                case 5:
                    return 13;
            }
        }

        public float GetMinDamage()
        {
            switch (level)
            {
                default:
                    return 38f;
                case 2:
                    return 48f;
                case 3:
                    return 66f;
                case 4:
                    return 78f;
                case 5:
                    return 92f;
            }
        }

        public float GetMaxDamage()
        {
            switch (level)
            {
                default:
                    return 64f;
                case 2:
                    return 78f;
                case 3:
                    return 92f;
                case 4:
                    return 114f;
                case 5:
                    return 130f;
            }
        }

        public override string GetDescription() => $"Conjure a fireball and launch it at an opponent, " +
            $"dealing {GetMinDamage()} - {GetMaxDamage()} fire damage on impact.";

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
