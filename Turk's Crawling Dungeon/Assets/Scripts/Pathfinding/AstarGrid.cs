using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace TCD.Pathfinding
{
    public struct AstarGrid : IDisposable
    {
        public int width;
        public int height;
        [DeallocateOnJobCompletion] public NativeArray<AstarNode> grid;
        public bool isInitialized;

        public AstarNode Get(int x, int y)
        {
            if (!IsWithin(x, y))
                return default;
            return grid[y * width + x];
        }

        public void Set(int x, int y, AstarNode node)
        {
            if (!IsWithin(x, y))
                return;
            grid[y * width + x] = node;
        }

        public bool IsWithin(int x, int y) => 
            (x >= 0 && x < width && y >= 0 && y < height);

        public NativeArray<AstarNode> GetNeighbors(AstarNode center)
        {
            int count = GetNeighborCount(center);
            NativeArray<AstarNode> neighbors = new NativeArray<AstarNode>(count, Allocator.Temp);
            int oX = center.x;
            int oY = center.y;

            int i = 0;
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int x = oX + xOffset;
                    int y = oY + yOffset;
                    if ((x == oX && y == oY) || !IsWithin(x, y))
                        continue;
                    neighbors[i] = (Get(x, y));
                    i++;
                }
            return neighbors;
        }

        private int GetNeighborCount(AstarNode center)
        {
            int count = 0;
            int oX = center.x;
            int oY = center.y;

            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int x = oX + xOffset;
                    int y = oY + yOffset;
                    if ((x == oX && y == oY) || !IsWithin(x, y))
                        continue;
                    count++;
                }
            return count;
        }

        public void Dispose()
        {
            //if (grid.IsCreated)
            //    grid.Dispose();
        }
    }
}
