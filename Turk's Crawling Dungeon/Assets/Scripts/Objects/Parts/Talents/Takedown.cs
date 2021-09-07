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
    public class Takedown : Talent
    {
        public override string Name => "Takedown";

        public override string TalentTree => "BasicGrappling";

        public override Sprite Icon => Assets.Get<Sprite>("TakedownIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost()
        {
            switch (level)
            {
                default:
                    return 16;
                case 2:
                    return 18;
                case 3:
                    return 20;
                case 4:
                    return 22;
                case 5:
                    return 23;
            }
        }

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 9 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 9 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
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

        public override int GetRange() => 1;

        public float GetUnarmedDamageMultiplier()
        {
            switch (level)
            {
                default:
                    return 1.2f;
                case 2:
                    return 1.45f;
                case 3:
                    return 1.75f;
                case 4:
                    return 2.15f;
                case 5:
                    return 2.4f;
            }
        }

        public override string GetDescription() => $"Make an unarmed attack against an opponent for {GetUnarmedDamageMultiplier() * 100}% " +
            $"damage. If you are grappling with the enemy, this attack will knock them prone.";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {

            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
