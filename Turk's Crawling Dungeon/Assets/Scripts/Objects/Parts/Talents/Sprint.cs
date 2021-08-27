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

        public override Sprite Icon => Assets.Get<Sprite>("SprintIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Toggle;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost() => 20;

        public override int GetSustainResourceCost() => 18;

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override bool Activate()
        {
            if (parent.parts.TryGet(out Effects.Effects effects))
                effects.AddEffect(new Sprinting(), TimeInfo.TIME_PER_STANDARD_TURN * 9999);
            return base.Activate();
        }

        public override bool Deactivate()
        {
            if (parent.parts.TryGet(out Effects.Effects effects))
                effects.RemoveEffect("Sprinting");
            return base.Deactivate();
        }

        public override int GetEnergyCost() => 0;

        public override int GetRange() => 1;

        public override string GetDescription() => $"While sprinting you move 100% " +
            $"faster at the cost of {GetSustainResourceCost()} stamina per turn.";
    }
}
