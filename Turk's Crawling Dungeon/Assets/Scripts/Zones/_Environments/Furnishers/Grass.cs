using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Zones.Utilities;

namespace TCD.Zones.Environments
{
    public class Grass : Furnisher
    {
        private const int MIN_SIZE = 8;
        private const int MAX_SIZE = 16;
        private const int FILL_CHANCE = 40;
        private const int SMOOTHING_STEPS = 2;
        
        private int width;
        private int height;

        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            width = RandomInfo.Random.Next(MIN_SIZE, MAX_SIZE);
            height = RandomInfo.Random.Next(MIN_SIZE, MAX_SIZE);
            Vector2Int startPosition = GetStartPosition();
            CellularAutomataGrid grid = new CellularAutomataGrid(width, height, FILL_CHANCE, SMOOTHING_STEPS);
            HashSet<Vector2Int> largestContiguousShape = CellularAutomataUtility.GetLargestContiguousShape(grid);
            foreach (Vector2Int position in largestContiguousShape)
            {
                Vector2Int realPosition = startPosition + position;
                if (PositionChecker.IsFloored(realPosition))
                    MakeGrass(realPosition);
            }
        }

        private Vector2Int GetStartPosition()
        {
            int xMin = currentFeature.BoundsInt.xMin;
            int xMax = currentFeature.BoundsInt.xMin;
            int yMin = currentFeature.BoundsInt.yMin;
            int yMax = currentFeature.BoundsInt.yMax;
            int x = ((xMin + xMax) / 2) - width / 2;
            int y = ((yMin + yMax) / 2) - height / 2;
            return new Vector2Int(x, y);
        }

        private void MakeGrass(Vector2Int position)
        {
            string obj = environment.GetRandomFurnishing("Grass");
            ObjectFactory.BuildFromBlueprint(obj, position);
        }
    }
}
