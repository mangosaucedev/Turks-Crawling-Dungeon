using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TCD
{
    public struct FixedHashMapEntry<TKey, TValue> : IEquatable<FixedHashMapEntry<TKey, TValue>>
        where TKey : struct, IEquatable<TKey>
        where TValue : struct
    {
        public TKey key;
        public TValue value;

        public FixedHashMapEntry(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public bool Equals(FixedHashMapEntry<TKey, TValue> other) => key.Equals(other.key);

    }
}
