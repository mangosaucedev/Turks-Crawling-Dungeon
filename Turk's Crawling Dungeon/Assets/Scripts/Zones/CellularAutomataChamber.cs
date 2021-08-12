using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Utilities;

namespace TCD.Zones
{
    public class CellularAutomataChamber : Chamber
    {
        private CellularAutomataGrid grid;

        private CellularAutomataGrid Grid
        {
            get
            {
                if (grid == null)
                    grid = new CellularAutomataGrid(Width, Height, 45f, 5);
                return grid;
            }
            set => grid = value;
        }

        public CellularAutomataChamber(int width, int height) : base(width, height)
        {
        }

        public override void Generate()
        {
            HashSet<Vector2Int> largestContiguousShape = 
                CellularAutomataUtility.GetLargestContiguousShape(Grid);
            if (largestContiguousShape.Count == 0)
            {
                Grid = null;
                Generate();
                return;
            }
            foreach (Vector2Int position in largestContiguousShape)
                Cells[position] = ChamberCellType.Floor;
        }
    }
}
