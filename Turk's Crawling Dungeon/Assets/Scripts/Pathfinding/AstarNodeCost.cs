using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public struct AstarNodeCost : INativeHeapItem<AstarNodeCost>
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public Vector2Int parent;

        public int HeapIndex { get; set; }

        public int FCost => gCost + hCost;

        public AstarNodeCost(Vector2Int position)
        {
            this.position = position;
            gCost = 0;
            hCost = 0;
            parent = -Vector2Int.one;

            HeapIndex = 0;
        }

        public int CompareTo(AstarNodeCost other)
        {
            int comparison = FCost.CompareTo(other.FCost);
            if (comparison == 0)
                comparison = hCost.CompareTo(other.hCost);
            return -comparison;
        }
    }
}
