using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.Zones
{
    public static class PositionChecker
    {
        private static GameGrid GameGrid => CurrentZoneInfo.grid;

        private static IZone Zone => CurrentZoneInfo.zone;

        public static bool IsEmpty(Vector2Int position) =>
            IsEmpty(position.x, position.y);

        public static bool IsEmpty(int x, int y)
        {
            if (!GameGrid.IsWithinBounds(x, y) || !IsFloored(x, y))
                return false;
            Cell cell = GameGrid[x, y];
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj.Parts.Get<Inspectable>())
                    return false;
            }
            return true;
        }

        public static bool IsEmptyInRadius(Vector2Int position, int xRadius = 1, int yRadius = 1) =>
            IsEmptyInRadius(position.x, position.y, xRadius, yRadius);

        public static bool IsEmptyInRadius(int x, int y, int xRadius = 1, int yRadius = 1)
        {
            int xMin = x - xRadius;
            int yMin = y - yRadius;
            int xMax = x + xRadius;
            int yMax = y + yRadius;
            for (int checkX = xMin; checkX <= xMax; checkX++)
                for (int checkY = yMin; checkY <= yMax; checkY++)
                {
                    if (!IsEmpty(checkX, checkY))
                        return false;
                }
            return true;
        }

        public static bool ContainsPart<T>(Vector2Int position, Predicate<T> predicate = null) where T : Part =>
            ContainsPart<T>(position.x, position.y, predicate);

        public static bool ContainsPart<T>(int x, int y, Predicate<T> predicate = null) where T : Part
        {
            if (!GameGrid.IsWithinBounds(x, y))
                return false;
            Cell cell = CurrentZoneInfo.grid[x, y];
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj.Parts.TryGet(out T part) && (predicate == null || predicate(part)))
                    return true;
            }
            return false;
        }

        public static bool IsDoor(Vector2Int position) => IsDoor(position.x, position.y);

        public static bool IsDoor(int x, int y) => ContainsPart<Door>(x, y, d => !d.IsLocked || d.IsOpen);

        public static bool IsObstacle(Vector2Int position) => IsObstacle(position.x, position.y);

        public static bool IsObstacle(int x, int y) => ContainsPart<Obstacle>(x, y, o => o.IsImpassable) && !IsDoor(x, y);

        public static bool IsFloored(Vector2Int position) => IsFloored(position.x, position.y);

        public static bool IsFloored(int x, int y)
        {
            if (!GameGrid.IsWithinBounds(x, y))
                return false;
            return Zone.CellTypes[x, y] >= ChamberCellType.Floor;
        }
    }
}
