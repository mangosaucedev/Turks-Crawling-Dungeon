using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD
{
    public class Cell : ILocalEventHandler
    {
        public const int SIZE = 2;

        public List<BaseObject> objects = new List<BaseObject>();

        private int x;
        private int y;   
        
        public int X => x;

        public int Y => y;

        public Vector2Int Position => new Vector2Int(x, y);

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Contains(BaseObject obj) => objects.Contains(obj);

        public bool Contains<T>() where T : Part => Contains(out T part);

        public bool Contains<T>(out T part) where T : Part
        {
            part = null;
            foreach (BaseObject obj in objects)
            {
                if (obj.parts.TryGet(out part))
                    return part;
            }
            return false;
        }

        public bool Add(BaseObject obj)
        {
            if (objects.Contains(obj))
                return false;
            objects.Add(obj);
            return true;
        }

        public bool Remove(BaseObject obj)
        {
            if (!objects.Contains(obj))
                return false;
            objects.Remove(obj);
            return true;
        }

        public bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent =>
            target.HandleEvent(e);

        public bool HandleEvent<T>(T e) where T : LocalEvent
        {
            bool isSuccessful = true;
            CleanupObjectList();
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = objects[i];
                if (!FireEvent(obj, e))
                    isSuccessful = false;
            }
            return isSuccessful;
        }

        private void CleanupObjectList()
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = objects[i];
                if (obj == null)
                    objects.RemoveAt(i);
            }
        }

        public List<BaseObject> GetVisibleObjects()
        {
            List<BaseObject> visibleObjects = new List<BaseObject>();
            foreach (BaseObject obj in objects)
            {
                if (obj.parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
                    visibleObjects.Add(obj);
            }
            return visibleObjects;
        }
    }
}
