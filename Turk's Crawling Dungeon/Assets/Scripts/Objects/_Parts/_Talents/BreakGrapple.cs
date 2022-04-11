using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class BreakGrapple : Talent
    {
        public override string Name => "Break Grapple";

        public override string TalentTree => "BasicAtheltics";

        public override string IconName => "BreakGrappleIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        protected override bool CanUseOnObject(BaseObject obj) => true;

        protected override void OnObject() => BreakAllGrapples();

        protected override bool CanUseOnCell(Cell cell) => true;

        protected override void OnCell() => BreakAllGrapples();
        
        private void BreakAllGrapples()
        {

        }

        public override int GetEnergyCost() => 0;

        public override int GetRange(int level) => 1;

        public override string GetDescription(int level) => $"Break your active grapples.";
    }
}
