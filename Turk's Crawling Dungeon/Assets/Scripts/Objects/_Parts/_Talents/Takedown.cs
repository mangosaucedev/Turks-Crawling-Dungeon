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
    public class Takedown : Talent
    {
        public override string Name => "Takedown";

        public override string TalentTree => "BasicGrappling";

        public override string IconName => "TakedownIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
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

        public override int GetCooldown(int level)
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

        protected override bool CanUseOnObject(BaseObject obj) => true;

        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => false;

        protected override void OnCell()
        {

        }

        public override List<ITalentRequirement> GetRequirements(int level)
        {
            List<ITalentRequirement> requirements = base.GetRequirements(level);
            requirements.Add(new RequiresTalentLevel(typeof(Grapple)));
            return requirements;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public float GetUnarmedDamageMultiplier(int level)
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

        public override string GetDescription(int level) => $"Make an unarmed attack against an opponent for {GetUnarmedDamageMultiplier(level) * 100}% " +
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
