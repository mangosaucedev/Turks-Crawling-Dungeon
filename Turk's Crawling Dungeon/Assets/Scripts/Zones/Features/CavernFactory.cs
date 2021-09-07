using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public static class CavernFactory
    {
        public static Cavern BuildCavern(int x, int y, int width, int height, int minSize)
        {
            return new Cavern(x, y, width, height, minSize);
        }
    }
}
