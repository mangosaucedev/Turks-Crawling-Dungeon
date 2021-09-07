using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class Cavern : IFeature
    {
        private const int FILL_CHANCE = 45;
        private const int MAX_ATTEMPTS = 128;

        public int x;
        public int y;
        public int width;
        public int height;

        private int minSize;
        private BoundsInt boundsInt;
        private HashSet<Vector2Int> occupiedPositions;
        private Environment environment;

        public BoundsInt BoundsInt
        {
            get => boundsInt;
            set => boundsInt = value;
        }

        public HashSet<Vector2Int> OccupiedPositions
        {
            get => occupiedPositions;
            set => occupiedPositions = value;
        }

        public Environment Environment
        {
            get => environment;
            set => environment = value;
        }

        public Cavern(int x, int y, int width, int height, int minSize)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.minSize = minSize;
            GenerateCavern();
        }

        private void GenerateCavern()
        {
            int attempts = 0;
            while (OccupiedPositions == null || OccupiedPositions?.Count < minSize)
            {
                CellularAutomataGrid grid = new CellularAutomataGrid(width, height, FILL_CHANCE);
                OccupiedPositions = CellularAutomataUtility.GetLargestContiguousShape(grid);
                BoundsInt = new BoundsInt(new Vector3Int(x, y, 0), new Vector3Int(width, height, 0));
                attempts++;
                if (attempts > MAX_ATTEMPTS)
                {
                    DebugLogger.LogError("Could not generate cavern of size " + minSize + ", settling for " + OccupiedPositions.Count);
                    break;
                }
            }
        }
    }
}
