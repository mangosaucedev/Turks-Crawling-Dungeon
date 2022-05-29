using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Pathfinding;

namespace TCD.Objects.Parts
{
    public struct MoveToPathfindingJobParams : IDisposable
    {
        public Guid goalId;
        public AstarGrid grid;
        public AstarPath path;
        public Vector2Int startPosition;
        public Vector2Int targetPosition;

        public void Dispose()
        {
            grid.Dispose();
            path.Dispose();
        }
    }
}
