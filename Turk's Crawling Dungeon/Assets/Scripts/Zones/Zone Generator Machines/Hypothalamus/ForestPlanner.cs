using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class ForestPlanner : ZoneGeneratorMachine
    {
        private const float FOREST_FILL_CHANCE = 40f;
        private const int FOREST_STEPS = 8;

        public override IEnumerator Generate()
        {
            IFeature feature = new Cavern(0, 0, Width, Height, 512);
            feature.BoundsInt = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(Width, Height, 1));
            TGrid<bool> cellularAutomata = 
                new CellularAutomataGrid(Width, Height, FOREST_FILL_CHANCE, FOREST_STEPS).Invert();
            HashSet<Vector2Int> largestShape = CellularAutomataUtility.GetLargestContiguousShape(cellularAutomata);

            for (int x = 0; x < Width; x++) 
                for (int y = 0; y < Height; y++)
                {
                    bool isFloor = largestShape.Contains(new Vector2Int(x, y));
                    
                    if ((x + y) % 128 == 0)
                        DebugLogger.Log($"@{x}, {y} is floor?: {isFloor} | current cell type: {Zone.CellTypes[x, y]}");

                    if (isFloor)
                    {
                        Zone.CellTypes[x, y] = ChamberCellType.Floor;
                        feature.OccupiedPositions.Add(new Vector2Int(x, y));
                    }
                }

            Zone.Features.Add(feature);

            yield return null;
        }
    }
}
