using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TCD
{
    public unsafe struct FixedList<T> : IDisposable where T : struct, IEquatable<T>
    {
        private IntPtr arrayPtr;
        private int sizeOfT;
        private int count;
        
        public int Count => count;

        public T this[int i]
        {
            get => (T)Marshal.PtrToStructure(arrayPtr + i * sizeOfT, typeof(T));

            set => Marshal.StructureToPtr(value, arrayPtr + i * sizeOfT, false);
        }

        public FixedList(int length)
        {
            sizeOfT = Marshal.SizeOf(typeof(T));
            arrayPtr = Marshal.AllocHGlobal(sizeOfT * length);
            count = 0;
        }

        public void Add(T item)
        {
            this[count] = item;
            count++;
        }

        public void Remove(T item)
        {
            bool itemFound = false;
            for (int i = 0; i < count; i++)
            {
                T o = this[i];
                
                if (itemFound)
                {
                    this[i - 1] = o;
                    this[i] = default;
                }
                
                if (item.Equals(o))
                {
                    this[i] = default;
                    itemFound = true;
                }
            }

            if (itemFound)
                count--;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < count; i++)
            {
                T o = this[i];

                if (item.Equals(o))
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(arrayPtr);
        }
    }
}
