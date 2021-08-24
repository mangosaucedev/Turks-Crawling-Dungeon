using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD
{
    public class BlockedByObstaclesEvaluator : GridRaycastEvaluator
    {
        public override bool IsTraversible(Vector2Int position)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            if (!grid.IsWithinBounds(position))
                return false;
            Cell cell = grid[position];
            if (cell.Contains(out Obstacle obstacle) && obstacle.IsImpassable)
                return false;
            return true;
        }
    }
}
