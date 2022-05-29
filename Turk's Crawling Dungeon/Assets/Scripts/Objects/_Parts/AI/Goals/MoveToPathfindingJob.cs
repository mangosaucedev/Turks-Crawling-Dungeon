using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using TCD.Pathfinding;
using TCD.Threading;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public struct MoveToPathfindingJob : IDisposableJob
    {
        public MoveToPathfindingJobParams parameters;

        private Guid guid;

        public Guid Guid => guid;

        public MoveToPathfindingJob(ref MoveToPathfindingJobParams parameters)
        {
            this.parameters = parameters;
            guid = Guid.NewGuid();
        }

        public void Execute()
        {
            if ((!parameters.path.isValid || parameters.path.targetPosition != parameters.targetPosition)
                && !TryToFindPathToTarget())
                return;

            PathfindingManager.AddPathEntry(new AstarPathEntry { id = parameters.goalId, path = parameters.path });
            JobManager.Dispose(this);
        }

        public bool TryToFindPathToTarget()
        {
            if (!parameters.grid.isInitialized)
                return false;

            parameters.path = new AstarPath(this, Allocator.Temp);
            return parameters.path.isValid;
        }

        public void Dispose() => parameters.Dispose();
    }
}
