using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class FreeEvaluator : GridRaycastEvaluator
    {
        public override bool IsTraversible(Vector2Int position) => true;
    }
}
