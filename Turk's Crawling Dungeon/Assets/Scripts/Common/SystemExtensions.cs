using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class SystemExtension
    {
        public static int RoundUpToInt(this float number)
        {
            return Mathf.FloorToInt(number + 0.5f);
        }

        public static int RoundDownToInt(this float number)
        {
            return Mathf.CeilToInt(number - 0.5f);
        }

        public static float RoundToDecimal(this float f, int decimals)
        {
            return (float)Math.Round((decimal)f, decimals);
        }

        public static float ToAngle(this Vector2 vector) =>
            Mathf.Atan2(vector.y, vector.x);
        

        public static float ToAngle(this Vector2Int vector) =>
            Mathf.Atan2(vector.y, vector.x);


        public static Vector2Int ToVectorIntNormalized(this Vector2 vector)
        {
            vector = vector.normalized;
            int x = Mathf.Clamp(Mathf.RoundToInt(vector.x), -1, 1);
            int y = Mathf.Clamp(Mathf.RoundToInt(vector.y), -1, 1);
            return new Vector2Int(x, y);
        }
        
        public static Direction ToDirection(this Vector2 vector)
        {
            float angle = vector.ToAngle();
            int octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
            if (octant == 0)
                return Direction.East;
            if (octant == 1)
                return Direction.NorthEast;
            if (octant == 2)
                return Direction.North;
            if (octant == 3) 
                return Direction.NorthWest;
            if (octant == 4)
                return Direction.West;
            if (octant == 5)
                return Direction.SouthWest;
            if (octant == 6)
                return Direction.South;
            return Direction.SouthEast;
        }

        public static Cardinal ToCardinal(this Vector2 vector)
        {
            float angle = vector.ToAngle();
            int quadrant = Mathf.RoundToInt(4 * angle / (2 * Mathf.PI) + 4) % 4;
            if (quadrant == 0)
                return Cardinal.East;
            if (quadrant == 1)
                return Cardinal.North;
            if (quadrant == 2)
                return Cardinal.West;
            return Cardinal.South;
        }

        public static Direction ToDirection(this Vector2Int vector)
        {
            if (vector == new Vector2Int(0, 1))
                return Direction.North;
            if (vector == Vector2Int.one)
                return Direction.NorthEast;
            if (vector == new Vector2Int(-1, 1))
                return Direction.NorthWest;
            if (vector == new Vector2Int(-1, 0))
                return Direction.West;
            if (vector == new Vector2Int(1, 0))
                return Direction.East;
            if (vector == new Vector2Int(0, -1))
                return Direction.South;
            if (vector == new Vector2Int(1, -1))
                return Direction.SouthEast;
            return Direction.SouthWest;
        }

        public static Cardinal ToCardinal(this Vector2Int vector)
        {
            float angle = vector.ToAngle();
            int quadrant = Mathf.RoundToInt(4 * angle / (2 * Mathf.PI) + 4) % 4;
            if (quadrant == 0)
                return Cardinal.East;
            if (quadrant == 1)
                return Cardinal.North;
            if (quadrant == 2)
                return Cardinal.West;
            return Cardinal.South;
        }
    }
}
