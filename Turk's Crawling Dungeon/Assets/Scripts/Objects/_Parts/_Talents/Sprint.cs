using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Sprint : Talent
    {
        public override string Name => "Sprint";

        public override string TalentTree => "BasicAthletics";

        public override string IconName => "SprintIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Toggle;

        public override TargetMode TargetMode => TargetMode.Attack;

        public static float GetSprintSpeedMultiplier(int level)
        {
            switch (level)
            {
                default:
                    return 2f;
                case 2:
                    return 2.1f;
                case 3:
                    return 2.3f;
                case 4:
                    return 2.5f;
                case 5:
                    return 2.7f;
            }
        }

        public override int GetActivationResourceCost(int level)
        {
            switch (level)
            {
                default:
                    return 20;
                case 2:
                    return 21;
                case 3:
                    return 22;
                case 4:
                    return 23;
                case 5:
                    return 25;
            }
        }


        public override int GetSustainResourceCost(int level)
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
                    return 23;
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

        public override bool Activate()
        {
            if (parent.Parts.TryGet(out Effects.Effects effects))
            {
                effects.AddEffect(new Sprinting(level), TimeInfo.TIME_PER_STANDARD_TURN * 9999);
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You are sprinting!");
            }
            return base.Activate();
        }

        public override bool Deactivate()
        {
            if (parent.Parts.TryGet(out Effects.Effects effects))
            {
                effects.RemoveEffect("Sprinting");
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You are no longer sprinting.");
            }
            return base.Deactivate();
        }

        public override int GetEnergyCost() => 0;

        public override int GetRange(int level) => 1;

        public override string GetDescription(int level) => $"While sprinting you move {(GetSprintSpeedMultiplier(level) - 1f) * 100f}% " +
            $"faster at the cost of {GetSustainResourceCost(level)} stamina per turn.";
    }
}
