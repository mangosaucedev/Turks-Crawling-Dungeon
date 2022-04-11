using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.Texts;

namespace TCD.Objects.Parts.Talents
{
    public class Pin : Talent
    {
        public override string Name => "Pin";

        public override string TalentTree => "BasicGrappling";

        public override string IconName => "PinIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
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

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!obj.Parts.Has(typeof(Combat)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't choke this!", Color.red);
                return false;
            }
            if (!obj.Parts.TryGet(out Effects.Effects effects))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Target immune!", Color.red);
                return false;
            }
            return true;
        }

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
            requirements.Add(new RequiresTalentLevel(typeof(Takedown)));
            return requirements;
        }
        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public float GetUnarmedDamageMultiplier(int level)
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

        public int GetPinDuration(int level)
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

        public int GetDisarmDuration(int level)
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

        public override string GetDescription(int level) => $"Make an unarmed attack against an opponent for {GetUnarmedDamageMultiplier(level) * 100}% " +
            $"damage. If you are grappling with the enemy, this attack will pin them for {((float) GetPinDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns. If the grappled enemy is also prone, they will be disarmed for {((float) GetDisarmDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
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
