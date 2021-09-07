using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Utilities
{
    public static class CellularAutomataUtility 
    {
        private const bool EMPTY = true;

        private static HashSet<Vector2Int> searched = new HashSet<Vector2Int>();
        private static HashSet<Vector2Int> largestContiguousShape = new HashSet<Vector2Int>();
        private static CellularAutomataGrid currentGrid;

        public static HashSet<Vector2Int> GetLargestContiguousShape(CellularAutomataGrid grid)
        {
            currentGrid = grid;
            searched.Clear();
            largestContiguousShape.Clear();
            for (int x = 0; x < grid.width; x++)
                for (int y = 0; y < grid.height; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    if (searched.Contains(position))
                        continue;
                    bool value = grid[x, y];
                    if (value == EMPTY)
                        FloodFillForLargest(x, y);
                }
            return largestContiguousShape;
        }

        private static void FloodFillForLargest(int xStart, int yStart)
        {
            HashSet<Vector2Int> contiguousShape = FloodFill(xStart, yStart);
            if (contiguousShape.Count > largestContiguousShape.Count)
                largestContiguousShape = contiguousShape;
        }

        private static HashSet<Vector2Int> FloodFill(int xStart, int yStart)
        {
            HashSet<Vector2Int> contiguousShape = new HashSet<Vector2Int>();
            Vector2Int position = new Vector2Int(xStart, yStart);
            Queue<Vector2Int> floodQueue = new Queue<Vector2Int>();
            searched.Add(position);
            floodQueue.Enqueue(position);
            while (floodQueue.Count > 0)
            {
                contiguousShape.Add(position);
                position = floodQueue.Dequeue();
                TryEnqueuePositionNeighbors(floodQueue, position);
            }
            return contiguousShape;
        }

        private static void TryEnqueuePositionNeighbors(Queue<Vector2Int> queue, Vector2Int position)
        {
            int xMin = position.x - 1;
            int xMax = position.x + 1;
            int yMin = position.y - 1;
            int yMax = position.y + 1;
            for (int x = xMin; x <= xMax; x++)
                for (int y = yMin; y <= yMax; y++)
                {
                    Vector2Int checkPosition = new Vector2Int(x, y);
                    if (currentGrid.IsWithinBounds(checkPosition) &&
                        !(x == position.x && y == position.y) &&
                        !searched.Contains(checkPosition) &&
                        currentGrid[position] == EMPTY)
                    {
                        searched.Add(checkPosition);
                        queue.Enqueue(checkPosition);
                    }
                }
        }
    }
}
