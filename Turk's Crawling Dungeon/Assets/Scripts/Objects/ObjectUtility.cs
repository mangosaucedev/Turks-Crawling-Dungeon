using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public static class ObjectUtility 
    {
        public static Vector2Int GetDirectionToObject(BaseObject fromObject, BaseObject toObject)
        {
            Vector2Int from = fromObject.cell.Position;
            Vector2Int to = toObject.cell.Position;
            Vector2Int unclampedDirection = to - from;
            float roundedX = Mathf.Round(unclampedDirection.x);
            float roundedY = Mathf.Round(unclampedDirection.y);
            int xDirection = (int)Mathf.Clamp(roundedX, -1, 1);
            int yDirection = (int)Mathf.Clamp(roundedY, -1, 1);
            return new Vector2Int(xDirection, yDirection);
        }

        public static int GetDistanceToObject(BaseObject fromObject, BaseObject toObject)
        {
            Vector2Int from = fromObject.cell.Position;
            Vector2Int to = toObject.cell.Position;
            return Mathf.FloorToInt(Vector2Int.Distance(from, to));
        }
    }
}
