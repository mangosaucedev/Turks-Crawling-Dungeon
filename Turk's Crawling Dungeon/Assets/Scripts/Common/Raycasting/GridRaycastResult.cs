using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GridRaycastResult 
    { 
        public GridRay ray;
        public bool collision;
        public Vector2Int collisionPoint;
        public int collisionIndex;

        private GridRaycastEvaluator evaluator;

        public GridRaycastResult(GridRay ray, GridRaycastEvaluator evaluator)
        {
            this.ray = ray;
            this.evaluator = evaluator;
            EvaluateRay();
        }

        private void EvaluateRay()
        {
            int count = ray.positions.Count;
            if (count == 1)
                EvaluatePosition(0, ray.positions[0]);
            for (int i = 1; i < count; i++)
            {
                Vector2Int position = ray.positions[i];
                EvaluatePosition(i, position);
            }
        }

        private void EvaluatePosition(int index, Vector2Int position)
        {
            if (!evaluator.IsTraversible(position) && !collision)
            {
                collisionIndex = index;
                collisionPoint = position;
                collision = true;
            }
        }
    }
}
