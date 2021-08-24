using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public abstract class GridRaycastEvaluator
    {
        public abstract bool IsTraversible(Vector2Int position);
    }
}
