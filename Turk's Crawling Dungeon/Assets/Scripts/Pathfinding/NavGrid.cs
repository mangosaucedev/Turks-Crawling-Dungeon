using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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
                if (current == null)
                    current = CurrentZoneInfo.navGrid;
                return current;
            }
        }

        public NavGrid(int _width, int _height) : base(_width, _height)
        {
            current = this;
            Fill();
        }

        public static AstarGrid GetObjectAstarGrid(INavEvaluator evaluator) => Current.BuildObjectAstarGrid(evaluator);

        private AstarGrid BuildObjectAstarGrid(INavEvaluator evaluator)
        {
            var astarGrid = new AstarGrid
            {
                width = width,
                height = height,
                grid = new NativeArray<AstarNode>(width * height, Allocator.TempJob),
                isInitialized = false
            };
            
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    NavNode node = Get(x, y);
                    AstarNode nativeNode = new AstarNode(x, y);
                    nativeNode.difficulty = evaluator.GetDifficulty(node);
                    nativeNode.isPassable = evaluator.IsPassable(node);
                    astarGrid.Set(x, y, nativeNode);
                }

            astarGrid.isInitialized = true;
            return astarGrid;
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