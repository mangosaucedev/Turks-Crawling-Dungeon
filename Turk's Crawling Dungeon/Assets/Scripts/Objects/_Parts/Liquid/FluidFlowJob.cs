using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using TCD.Threading;

namespace TCD.Objects.Parts
{
    public struct FluidFlowJob : IDisposableJob
    {
        private Guid id;
        private Guid guid;
        private float flowMultiplier;

        public Guid Guid => guid;

        public FluidFlowJob(Guid id, float flowMultiplier)
        {
            this.id = id;
            this.flowMultiplier = flowMultiplier;
            guid = Guid.NewGuid();
        }

        public void Execute()
        {
            if (!PartUtility.TryGetFromGuid(id, out Liquid liquid))
                return;
            if (liquid.FlowToAdjacentCells(flowMultiplier))
                liquid.isFlowing = true;
            JobManager.Dispose(this);
        }

        public void Dispose()
        {

        }
    }
}
