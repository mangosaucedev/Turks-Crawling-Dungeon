using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class AIDistance
    {
        private Brain brain;

        public AIDistance(Brain brain)
        {
            this.brain = brain;
        }

        public int GetDistanceToInstanceInCells(BaseObject target)
        {
            Vector2Int position = brain.Position;
            Vector2Int targetPosition = target.cell.Position;
            float distance = Vector2Int.Distance(position, targetPosition);
            return Mathf.FloorToInt(distance);
        }
    }
}