using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Weave : Talent
    {
        public override string Name => "Weave";

        public override string TalentTree => "BasicAthletics";

        public override string IconName => "WeaveIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        public static int GetEvasion(int level)
        {
            switch (level)
            {
                default:
                    return 30;
                case 2:
                    return 35;
                case 3:
                    return 42;
                case 4:
                    return 49;
                case 5:
                    return 56;
            }
        }

        public override List<ITalentRequirement> GetRequirements(int level)
        {
            var requirements = base.GetRequirements(level);
            requirements.Add(new RequiresTalentLevel(typeof(Sprint)));
            return requirements;
        }

        public override int GetActivationResourceCost(int level)
        {
            switch (level)
            {
                default:
                    return 14;
                case 2:
                    return 16;
                case 3:
                    return 18;
                case 4:
                    return 20;
                case 5:
                    return 22;
            }
        }

        protected override bool CanUseOnObject(BaseObject obj) => false;

        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => false;

        protected override void OnCell()
        {

        }

        public override bool Activate()
        {
            if (activeCooldown <= 0f && parent.Parts.TryGet(out Effects.Effects effects))
            {
                if (effects.HasEffect("Evading"))
                    effects.RemoveEffect("Evading");
                effects.AddEffect(new Evading(level), GetDuration(level));
                activeCooldown = GetCooldown(level);
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You duck and weave the enemy's blows!");
                return true;
            }
            return false;
        }

        public override int GetEnergyCost() => 0;

        public override int GetRange(int level) => 1;

        public override string GetDescription(int level) => $"You duck and weave around enemy " +
            $"attacks, granting you {GetEvasion(level)}% evasion for {((float) GetDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns.";

        public int GetDuration(int level)
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
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 25 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 23 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 21 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 18 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 16 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }
    }
}
