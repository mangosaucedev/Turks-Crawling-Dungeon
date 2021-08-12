using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class DirectionTo 
    {
        public static Vector2Int Vector(Direction direction)
        {
            if (direction == Direction.North)
                return new Vector2Int(0, 1);
            if (direction == Direction.NorthEast)
                return Vector2Int.one;
            if (direction == Direction.NorthWest)
                return new Vector2Int(-1, 1);
            if (direction == Direction.West)
                return new Vector2Int(-1, 0);
            if (direction == Direction.East)
                return new Vector2Int(1, 0);
            if (direction == Direction.South)
                return new Vector2Int(0, -1);
            if (direction == Direction.SouthEast)
                return new Vector2Int(1, -1);
            return -Vector2Int.one;
        }

        public static Direction Opposite(Direction direction)
        {
            if (direction == Direction.North)
                return Direction.South;
            if (direction == Direction.NorthEast)
                return Direction.SouthWest;
            if (direction == Direction.NorthWest)
                return Direction.SouthEast;
            if (direction == Direction.West)
                return Direction.East;
            if (direction == Direction.East)
                return Direction.West;
            if (direction == Direction.South)
                return Direction.North;
            if (direction == Direction.SouthEast)
                return Direction.NorthWest;
            return Direction.NorthEast;
        }

        public static Cardinal ClosestCardinal(Direction direction)
        {
            if (direction == Direction.North)
                return Cardinal.North;
            if (direction == Direction.NorthEast)
                return Cardinal.North;
            if (direction == Direction.NorthWest)
                return Cardinal.West;
            if (direction == Direction.West)
                return Cardinal.West;
            if (direction == Direction.East)
                return Cardinal.East;
            if (direction == Direction.South)
                return Cardinal.South;
            if (direction == Direction.SouthEast)
                return Cardinal.East;
            return Cardinal.South;
        }
    }
}
