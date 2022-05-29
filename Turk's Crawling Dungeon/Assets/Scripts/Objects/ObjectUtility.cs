using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public static class ObjectUtility 
    {
        private static Dictionary<Guid, BaseObject> objects = new Dictionary<Guid, BaseObject>();

        public static void Add(BaseObject baseObject)
        {
            if (baseObject == null)
                return;
            objects[baseObject.id] = baseObject;
        }

        public static void Remove(Guid id) => objects.Remove(id);

        public static bool TryGetFromId(Guid id, out BaseObject baseObject) =>
            objects.TryGetValue(id, out baseObject);

        public static Vector2Int GetDirectionToObject(BaseObject fromObject, BaseObject toObject)
        {
            Vector2Int from = fromObject.cell.Position;
            Vector2Int to = toObject.cell.Position;
            return GetDirectionToVector(from, to);
        }

        public static Vector2Int GetDirectionToVector(Vector2Int from, Vector2Int to)
        {
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

        public static List<BaseObject> FindAllWithPart<T>(Predicate<T> predicate = null) where T : Part
        { 
            List<BaseObject> matchingObjects = new List<BaseObject>();

            foreach (BaseObject obj in BaseObject.enabledObjects)
            {
                if (obj.Parts.TryGet(out T part) && (predicate == null || predicate(part)))
                    matchingObjects.Add(obj);
            }

            return matchingObjects;
        }
    }
}
