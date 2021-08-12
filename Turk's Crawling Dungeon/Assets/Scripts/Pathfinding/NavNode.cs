using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public class NavNode : IHeapItem<NavNode>
    {
        public Vector2Int position;
        public int gCost;
        public int hCost;
        public NavNode parent;

        public int HeapIndex { get; set; }

        public int X => position.x;

        public int Y => position.y;

        public int FCost => gCost + hCost;

        public NavNode(Vector2Int position)
        {
            this.position = position;
        }

        public List<NavNode> GetNeighbors()
        {
            List<NavNode> neighbors = new List<NavNode>();

            for (int xoffset = -1; xoffset <= 1; xoffset++)
                for (int yoffset = -1; yoffset <= 1; yoffset++)
                {
                    Vector2Int checkPosition = new Vector2Int(position.x + xoffset, position.y + yoffset);
                    if (checkPosition == position || !NavGrid.Current.IsWithinBounds(checkPosition))
                        continue;
                    NavNode node = NavGrid.Current[checkPosition];
                    neighbors.Add(node);
                }

            return neighbors;
        }

        public void ResetCost()
        {
            parent = null;
            hCost = 0;
            gCost = 0;
        }

        public int CompareTo(NavNode other)
        {
            int comparison = FCost.CompareTo(other.FCost);
            if (comparison == 0)
                comparison = hCost.CompareTo(other.hCost);
            return -comparison;
        }
    }
}