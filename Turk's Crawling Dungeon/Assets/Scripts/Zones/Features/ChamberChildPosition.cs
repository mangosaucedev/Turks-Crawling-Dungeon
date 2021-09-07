using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ChamberChildPosition : IHeapItem<ChamberChildPosition>
    {
        private Vector2Int origin;

        public Vector2Int position;

        public int HeapIndex { get; set; }

        public float Distance => Vector2Int.Distance(position, origin);

        public ChamberChildPosition(Vector2Int origin, Vector2Int position)
        {
            this.origin = origin;
            this.position = position;
        }

        public int CompareTo(ChamberChildPosition other)
        {
            if (other == null)
                return -1;
            int comparison = Distance.CompareTo(other.Distance);
            return -comparison;
        }
    }
}
