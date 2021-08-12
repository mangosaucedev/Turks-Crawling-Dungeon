using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Pathfinding
{
    public class NavGrid : TGrid<NavNode>
    {
        private static NavGrid current;

        public static NavGrid Current
        {
            get
            {
                return current;
            }
        }

        public NavGrid(int _width, int _height) : base(_width, _height)
        {
            current = this;
            Fill();
        }

        private void Fill()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    NavNode node = new NavNode(position);
                    Set(x, y, node);
                }
        }

        public static NavNode WorldToNode(Vector3 worldPosition)
        {
            Vector2Int gridPosition = GameGrid.WorldToGrid(worldPosition);
            return Current.Get(gridPosition);
        }
    }
}