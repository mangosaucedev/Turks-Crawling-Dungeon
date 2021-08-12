using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class CardinalTo
    {
        public static Cardinal Opposite(Cardinal cardinal)
        {
            if (cardinal == Cardinal.North)
                return Cardinal.South;
            if (cardinal == Cardinal.East)
                return Cardinal.West;
            if (cardinal == Cardinal.South)
                return Cardinal.North;
            return Cardinal.East;
        }
    }
}
