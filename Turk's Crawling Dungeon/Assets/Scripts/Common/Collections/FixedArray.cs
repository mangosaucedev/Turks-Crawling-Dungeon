using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TCD
{
    public unsafe struct FixedArray<T> : IDisposable where T : struct
    {
        private IntPtr arrayPtr;
        private int sizeOfT;

        public T this[int i]
        {
            get => (T) Marshal.PtrToStructure(arrayPtr + i * sizeOfT, typeof(T));
            
            set => Marshal.StructureToPtr(value, arrayPtr + i * sizeOfT, false);
        }

        public FixedArray(int length)
        {
            sizeOfT = Marshal.SizeOf(typeof(T));
            arrayPtr = Marshal.AllocHGlobal(sizeOfT * length);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(arrayPtr);
        }
    }
}
