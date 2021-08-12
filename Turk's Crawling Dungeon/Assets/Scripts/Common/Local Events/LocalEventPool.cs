using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class LocalEventPool 
    {
        private Dictionary<Type, List<LocalEvent>> pools = 
            new Dictionary<Type, List<LocalEvent>>();

        public T Get<T>() where T : LocalEvent, new()
        {
            Type type = typeof(T);
            List<LocalEvent> pool = GetPool(type);
            if (pool.Count > 0)
            {
                T e = (T)pool[0];
                if (e != null)
                {
                    pool.RemoveAt(0);
                    return e;
                }
            }
            return new T();
        }

        private List<LocalEvent> GetPool(Type type)
        {
            if (!pools.TryGetValue(type, out List<LocalEvent> pool))
            {
                pool = new List<LocalEvent>();
                pools[type] = pool;
            }
            return pool;
        }

        public bool Return(LocalEvent e)
        {
            Type type = e.GetType();
            List<LocalEvent> pool = GetPool(type);
            if (pool.Contains(e))
                return false;
            pool.Add(e);
            return true;
        }
    }
}