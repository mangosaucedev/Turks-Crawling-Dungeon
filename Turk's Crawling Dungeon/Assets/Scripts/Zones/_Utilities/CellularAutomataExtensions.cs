using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Utilities
{
    public static class CellularAutomataExtensions
    {
        public static TGrid<bool> Invert(this TGrid<bool> grid)
        {
            for (int x = 0; x < grid.width; x++)
                for (int y = 0; y < grid.height; y++)
                {
                    grid[x, y] = !grid[x, y];
                }
            return grid;
        }
    }
}
