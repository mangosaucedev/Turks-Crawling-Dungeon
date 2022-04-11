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
    public class BloodCrazed : Talent
    {
        public override string Name => "Blood-Crazed";

        public override string TalentTree => "Bloodthirst";

        public override string IconName => "BloodCrazedIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Passive;

        public override TargetMode TargetMode => TargetMode.None;

        protected override bool CanUseOnObject(BaseObject obj) => false;

        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => false;

        protected override void OnCell()
        {
            
        }

        public override string GetDescription(int level) => $"Each time you make an attack " +
            $"against a bleeding enemy, you go into a Blood Craze, increasing your " +
            $"physical power by {GetPhysicalPowerBonus(level)} and your attack speed by " +
            $"{(1 - GetAttackCostMultiplier(level)) * 100}% for 2 turns.";

        private int GetPhysicalPowerBonus(int level)
        {
            switch (level)
            {
                default:
                    return 6;
                case 2:
                    return 12;
                case 3:
                    return 16;
                case 4:
                    return 19;
                case 5:
                    return 22;
            }
        }

        private float GetAttackCostMultiplier(int level)
        {
            switch (level)
            {
                default:
                    return 0.94f;
                case 2:
                    return 0.91f;
                case 3:
                    return 0.88f;
                case 4:
                    return 0.85f;
                case 5:
                    return 0.83f;
            }
        }

        protected override bool OnAttack(AttackEvent e)
        {
            if (e.defender.Parts.TryGet(out Effects.Effects targetEffects) && targetEffects.HasEffect("Bleeding") &&
                parent.Parts.TryGet(out Effects.Effects attackerEffects))
                attackerEffects.AddEffect(new BloodCraze(GetPhysicalPowerBonus(level), GetAttackCostMultiplier(level)), TimeInfo.TIME_PER_STANDARD_TURN * 2);
            return base.OnAttack(e);
        }
    }
}