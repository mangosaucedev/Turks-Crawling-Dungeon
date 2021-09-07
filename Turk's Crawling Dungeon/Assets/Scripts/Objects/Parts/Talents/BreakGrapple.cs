using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public class BreakGrapple : Talent
    {
        public override string Name => "Break Grapple";

        public override string TalentTree => "BasicAtheltics";

        public override Sprite Icon => Assets.Get<Sprite>("BreakGrappleIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            BreakAllGrapples();
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            BreakAllGrapples();
            yield break;
        }

        private void BreakAllGrapples()
        {

        }

        public override int GetEnergyCost() => 0;

        public override int GetRange() => 1;

        public override string GetDescription() => $"Break your active grapples.";
    }
}
