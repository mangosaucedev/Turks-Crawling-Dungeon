using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class Quadrant
    {
        public Cardinal cardinal;
        public Vector2Int origin;

        public Quadrant(Cardinal _cardinal, Vector2Int _origin)
        {
            cardinal = _cardinal;
            origin = _origin;
        }

        public Vector2Int LocalToGridPosition(Vector2Int localPosition)
        {
            int position = localPosition.x;
            int depth = localPosition.y;
            if (cardinal == Cardinal.North)
                return new Vector2Int(origin.x + position, origin.y + depth);
            else if (cardinal == Cardinal.South)
                return new Vector2Int(origin.x + position, origin.y - depth);
            else if (cardinal == Cardinal.East)
                return new Vector2Int(origin.x + depth, origin.y - position);
            else 
                return new Vector2Int(origin.x - depth, origin.y - position);
        }
    }
}