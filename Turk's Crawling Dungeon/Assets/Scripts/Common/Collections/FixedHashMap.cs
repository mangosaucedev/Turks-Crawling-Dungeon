using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TCD
{
    public unsafe struct FixedHashMap<TKey, TValue> : IDisposable 
        where TKey : struct, IEquatable<TKey> 
        where TValue : struct
    {
        private FixedList<FixedHashMapEntry<TKey, TValue>> entries;

        public TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }

        public FixedHashMap(int length)
        {
            entries = new FixedList<FixedHashMapEntry<TKey, TValue>>(length);
        }

        private TValue Get(TKey key)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                if (entry.key.Equals(key))
                    return entry.value;
            }
            return default;
        }

        private void Set(TKey key, TValue value)
        {
            FixedHashMapEntry<TKey, TValue> entry = default;

            for (int i = 0; i < entries.Count; i++)
            {
                entry = entries[i];
                if (entry.key.Equals(key))
                {
                    entry.value = value;
                    return;
                }
            }

            entry = new FixedHashMapEntry<TKey, TValue>(key, value);
            entries.Add(entry);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            value = default;
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                if (entry.key.Equals(key))
                {
                    value = entry.value;
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            entries.Dispose();
        }
    }
}
