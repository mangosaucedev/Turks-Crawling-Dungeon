using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    //TODO: Evaluate benefits of culling objects
    [Serializable]
    public class CullAtDistance : Part
    {
        public override string Name => "Cull at Distance";

        protected override void Start()
        {
            base.Start();
            ObjectCuller.objectsToCull.Add(parent);
        }

        private void OnDestroy()
        {
            ObjectCuller.objectsToCull.Remove(parent);
        }
    }
}
