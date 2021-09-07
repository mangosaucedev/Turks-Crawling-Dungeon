using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class CavePlanner : ZoneGeneratorMachine
    {
        public override IEnumerator Generate()
        {
            CellularAutomataGrid grid = new CellularAutomataGrid(Width, Height, 50, 10);
            HashSet<Vector2Int> largestContiguousShape = CellularAutomataUtility.GetLargestContiguousShape(grid);
            foreach (Vector2Int position in largestContiguousShape)
                Zone.CellTypes[position] = ChamberCellType.Floor;
            yield break;
        }
    }
}
