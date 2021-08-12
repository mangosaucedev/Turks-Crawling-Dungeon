using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public static class CorridorFactory
    {
        public static ICorridor Build(ChamberAnchor start, ChamberAnchor end)
        {
            return new RectangleCorridor(start, end);
        }
    }
}
