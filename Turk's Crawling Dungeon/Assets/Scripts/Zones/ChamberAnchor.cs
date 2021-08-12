using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class ChamberAnchor
    {
        public IChamber chamber;
        public int x;
        public int y;
        public Cardinal direction;
    
        public Vector2Int Position => new Vector2Int(x, y);

        public Vector2Int PositionReal => new Vector2Int(chamber.X + x, chamber.Y + y);

        public int XReal => PositionReal.x;

        public int YReal => PositionReal.y;

        public ChamberAnchor(IChamber chamber, int x, int y, Cardinal direction)
        {
            this.chamber = chamber;
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
    }
}
