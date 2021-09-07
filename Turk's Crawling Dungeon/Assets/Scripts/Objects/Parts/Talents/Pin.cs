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
    public class Pin : Talent
    {
        public override string Name => "Pin";

        public override string TalentTree => "BasicGrappling";

        public override Sprite Icon => Assets.Get<Sprite>("PinIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost()
        {
            switch (level)
            {
                default:
                    return 12;
                case 2:
                    return 13;
                case 3:
                    return 14;
                case 4:
                    return 15;
                case 5:
                    return 16;
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
                    return 0.7f;
                case 2:
                    return 0.95f;
                case 3:
                    return 1.1f;
                case 4:
                    return 1.25f;
                case 5:
                    return 1.4f;
            }
        }

        public int GetPinDuration()
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

        public int GetDisarmDuration()
        {
            switch (level)
            {
                default:
                    return 2 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription() => $"Make an unarmed attack against an opponent for {GetUnarmedDamageMultiplier() * 100}% " +
            $"damage. If you are grappling with the enemy, this attack will pin them for {((float) GetPinDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns. If the grappled enemy is also prone, they will be disarmed for {((float) GetDisarmDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns.";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {

            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
