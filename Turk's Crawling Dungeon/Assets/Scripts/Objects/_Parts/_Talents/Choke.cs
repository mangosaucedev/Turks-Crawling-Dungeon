using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.Texts;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Choke : Talent
    {
        public override string Name => "Choke";

        public override string TalentTree => "BasicGrappling";

        public override string IconName => "ChokeIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
        {
            switch (level)
            {
                default:
                    return 18;
                case 2:
                    return 19;
                case 3:
                    return 20;
                case 4:
                    return 21;
                case 5:
                    return 21;
            }
        }

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
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
            requirements.Add(new RequiresTalentLevel(typeof(Pin)));
            return requirements;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public float GetUnarmedDamageMultiplier(int level)
        {
            switch (level)
            {
                default:
                    return 0.5f;
                case 2:
                    return 0.75f;
                case 3:
                    return 1f;
                case 4:
                    return 1.15f;
                case 5:
                    return 1.35f;
            }
        }

        public int GetChokeDuration(int level)
        {
            switch (level)
            {
                default:
                    return 2 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 2 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public float GetChokeDamage(int level)
        {
            switch (level)
            {
                default:
                    return 15;
                case 2:
                    return 24;
                case 3:
                    return 32;
                case 4:
                    return 40;
                case 5:
                    return 47;
            }
        }

        public override string GetDescription(int level) => $"Make an unarmed attack against an opponent for {GetUnarmedDamageMultiplier(level) * 100}% " +
            $"damage. If you are grappling the enemy when this attack hits, you choke them into unconsciousness for " +
            $"{((float) GetChokeDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} turns. Any subsequent attacks against this foe, " +
            $"while they are grappled and knocked out, will further strangle the life from them, causing them to deal an additional {GetChokeDamage(level)} pure " +
            $"damage.";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {

            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
