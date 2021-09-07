using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public class Grapple : Talent
    {
        public override string Name => "Grapple";

        public override string TalentTree => "BasicGrappling";

        public override Sprite Icon => Assets.Get<Sprite>("GrappleIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost()
        {
            switch (level)
            {
                default:
                    return 5;
                case 2:
                    return 5;
                case 3:
                    return 5;
                case 4:
                    return 5;
                case 5:
                    return 5;
            }
        }

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
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

        public override int GetRange() => 1;

        public override string GetDescription() => $"Make an unarmed attack against an opponent. " +
            $"If it hits, you will begin grappling with your foe, increasing both of your " +
            $"move cost by 300%. If either of you exits melee range, the grapple will end. " +
            $"You may only grapple with one enemy at a time.\nAutomatically trains the talent " +
            $"Break Grapple.\nUse Break Grapple to end a grapple prematurely.";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {

            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
