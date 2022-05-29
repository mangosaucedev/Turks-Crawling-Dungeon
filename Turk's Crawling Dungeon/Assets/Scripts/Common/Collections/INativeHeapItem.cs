using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface INativeHeapItem<T> : IComparable<T> where T : struct
    {
        int HeapIndex { get; set; }
    }
}
