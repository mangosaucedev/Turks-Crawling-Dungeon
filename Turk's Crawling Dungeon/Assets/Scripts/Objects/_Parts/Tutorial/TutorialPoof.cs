using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class TutorialPoof : Part
    {
        public override string Name => "Tutorial Poof";

        protected override void Start()
        {
            base.Start();
            ActionScheduler.EnqueueAction(parent, () => { parent.Destroy(); });
        }
    }
}
