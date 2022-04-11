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
    public class Grapple : Talent
    {
        public override string Name => "Grapple";

        public override string TalentTree => "BasicGrappling";

        public override string IconName => "GrappleIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
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

        public override int GetCooldown(int level)
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

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!obj.Parts.Has(typeof(Combat)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't maul this!", Color.red);
                return false;
            }
            if (!obj.Parts.Has(typeof(Effects.Effects)))
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

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public override string GetDescription(int level) => $"Make an unarmed attack against an opponent. " +
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
