using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public abstract class LocalEvent
    {
        public const int MAX_POOL_SIZE = 8192;

        public static readonly string id = "";

        private static LocalEventPool pool = new LocalEventPool();

        public abstract string Id { get; }


        public static T Get<T>() where T : LocalEvent, new() =>
            pool.Get<T>();

        public virtual bool ReturnToPool()
        {
            Reset();
            return pool.Return(this);
        }

        protected abstract void Reset();
    }
}