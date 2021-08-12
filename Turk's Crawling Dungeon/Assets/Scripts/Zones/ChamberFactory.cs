using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public static class ChamberFactory
    {
        public static IChamber Build(int width, int height)
        {
            int random = RandomInfo.Random.Next(0, 2);
            if (random < 1)
                return new RectangleChamber(width, height);
            else
                return new CellularAutomataChamber(width, height);
        }
    }
}
