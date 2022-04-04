using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Utilities;

namespace TCD.Pathfinding.AutoExplore
{
    public class AutoExploreGrid : TGrid<bool>
    {
        private HashSet<Vector2Int> zoneArea = new HashSet<Vector2Int>();

        private static AutoExploreGrid current;

        public static AutoExploreGrid Current
        {
            get
            {
                if (current == null)
                    current = CurrentZoneInfo.autoExploreGrid;
                return current;
            }
        }

        public AutoExploreGrid(int width, int height) : base(width, height)
        {
            zoneArea = CellularAutomataUtility.GetLargestContiguousShape(CurrentZoneInfo.floorGrid);
        }
    }
}
