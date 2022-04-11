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
    public class VenomStrike : Talent
    {
        public override string Name => "Venom Strike";

        public override string TalentTree => "Envenomation";

        public override string IconName => "VenomStrikeIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        protected override bool CanUseOnObject(BaseObject obj) => true;

        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => true;

        protected override void OnCell()
        {

        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public float GetVenomDamage(int level)
        {
            switch (level)
            {
                default:
                    return 8f;
                case 2:
                    return 11f;
                case 3:
                    return 14f;
                case 4:
                    return 18f;
                case 5:
                    return 20f;
            }
        }

        public int GetDuration(int level)
        {
            switch (level)
            {
                default:
                    return 2 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription(int level) => $"Make an unarmed attack against an opponent. " +
            $"If it hits, your opponent will be injected with venom dealing {GetVenomDamage(level)} poison " +
            $"damage per turn for {((float) GetDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
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
