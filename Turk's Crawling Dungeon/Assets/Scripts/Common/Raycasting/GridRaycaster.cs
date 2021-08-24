using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class GridRaycaster
    {
        public static GridRaycastResult Raycast(Vector2Int startPosition, Vector2Int endPosition, GridRaycastEvaluator evaluator)
        {
            GridRay ray = new GridRay(startPosition, endPosition);
            GridRaycastResult result = new GridRaycastResult(ray, evaluator);
            return result;
        }
    }
}
