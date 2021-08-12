using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Pathfinding;
using TCD.Zones;

namespace TCD
{
    public static class CurrentZoneInfo
    {
        public static IZone zone;
        public static GameGrid grid;
        public static NavGrid navGrid;
    }
}
