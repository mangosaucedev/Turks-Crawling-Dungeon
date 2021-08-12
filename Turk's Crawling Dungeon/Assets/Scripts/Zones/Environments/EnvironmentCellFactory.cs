using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public static class EnvironmentCellFactory  
    {
        public static EnvironmentCell Create(int x, int y)
        {
            return null;//new Mundane(x, y);
        }
    }
}
