using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Zones.Utilities;

namespace TCD.Zones.Environments
{
    public class Lake : Furnisher
    {
        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            int width = RandomInfo.Random.Next(8, 15);
            int height = RandomInfo.Random.Next(8, 15);
            GameGrid grid = CurrentZoneInfo.grid;
            CellularAutomataGrid cellularAutomataGrid = new CellularAutomataGrid(width, height, 55f, 3);
            string liquid = environment.GetRandomFurnishing("Lake");
            HashSet<Vector2Int> shape = CellularAutomataUtility.GetLargestContiguousShape(cellularAutomataGrid);
            foreach (Vector2Int lakePosition in shape)
            {
                Vector2Int position = new Vector2Int(x, y) + lakePosition;
                if (PositionChecker.IsEmpty(position))
                    ObjectFactory.BuildFromBlueprint(liquid, position);
            }
        }
    }
}
