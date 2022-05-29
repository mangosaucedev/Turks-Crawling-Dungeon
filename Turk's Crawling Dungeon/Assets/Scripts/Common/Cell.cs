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
        private readonly object _lock = new object();
        
        public List<BaseObject> Objects
        {
            get
            {
                lock (_lock)
                {
                    for (int i = objects.Count - 1; i >= 0; i--)
                    {
                        if (!objects[i])
                            objects.RemoveAt(i);
                    }
                    return objects;
                }
            }
        }

        public int X => x;

        public int Y => y;

        public Vector2Int Position => new Vector2Int(x, y);

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Contains(BaseObject obj)
        {
            lock (_lock)
                return Objects.Contains(obj);
        }
        public bool Contains<T>() where T : Part => Contains(out T part);

        public bool Contains<T>(out T part) where T : Part
        {
            lock (_lock)
            {
                part = null;
                foreach (BaseObject obj in Objects)
                {
                    if (obj.Parts.TryGet(out part))
                        return part;
                }
                return false;
            }
        }

        public bool Add(BaseObject obj)
        {
            if (Objects.Contains(obj))
                return false;
            Objects.Add(obj);
            return true;
        }

        public bool Remove(BaseObject obj)
        {
            if (!Objects.Contains(obj))
                return false;
            Objects.Remove(obj);
            return true;
        }

        public bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent
        {
            lock (_lock)
                return target.HandleEvent(e);
        }

        public bool HandleEvent<T>(T e) where T : LocalEvent
        {
            lock (_lock)
            {
                bool isSuccessful = true;
                CleanupObjectList();
                for (int i = Objects.Count - 1; i >= 0; i--)
                {
                    BaseObject obj = Objects[i];
                    if (!FireEvent(obj, e))
                        isSuccessful = false;
                }
                return isSuccessful;
            }
        }

        private void CleanupObjectList()
        {
            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = Objects[i];
                if (obj == null)
                    Objects.RemoveAt(i);
            }
        }

        public List<BaseObject> GetVisibleObjects()
        {
            List<BaseObject> visibleObjects = new List<BaseObject>();
            foreach (BaseObject obj in Objects)
            {
                if (obj.Parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
                    visibleObjects.Add(obj);
            }
            return visibleObjects;
        }
    }
}
