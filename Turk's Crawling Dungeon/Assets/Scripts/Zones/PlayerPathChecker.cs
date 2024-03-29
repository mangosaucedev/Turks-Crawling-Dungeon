using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Pathfinding;

namespace TCD.Zones
{
    public static class PlayerPathChecker
    {
        public static Vector2Int endRoomPosition = Vector2Int.zero;

        public static bool TryGetValidPathToPoint(Vector2Int targetPosition, out NavAstarPath path)
        {
            Vector2Int playerPosition = GetPlayerPosition();
            path = new NavAstarPath(playerPosition, targetPosition, new PlayerPathEvaluator());
            return path.isValid;
        }

        public static bool ValidPathToPoint(Vector2Int targetPosition)
        {
            Vector2Int playerPosition = GetPlayerPosition();
            NavAstarPath path =
                new NavAstarPath(playerPosition, targetPosition, new PlayerPathEvaluator());
            return path.isValid;
        }

        public static bool ValidPathToEndRoom()
        {
            Vector2Int playerPosition = GetPlayerPosition();
            Vector2Int targetPosition = GetEndRoomPosition();
            NavAstarPath pathToEndRoom = 
                new NavAstarPath(playerPosition, targetPosition, new PlayerPathEvaluator());
            return pathToEndRoom.isValid;
        }

        private static Vector2Int GetPlayerPosition()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            return player.cell.Position;
        }

        private static Vector2Int GetEndRoomPosition()
        {
            if (endRoomPosition == Vector2Int.zero)
                endRoomPosition = FindDownStairsPosition();    
            return endRoomPosition;
        }

        private static Vector2Int FindDownStairsPosition()
        {
            IZone zone = CurrentZoneInfo.zone;
            IFeature feature;
            if (zone.Chambers.Count > 0)
                feature = zone.Chambers[zone.Chambers.Count - 1];
            else 
                feature = zone.Features[zone.Features.Count - 1];
            int xMin = feature.BoundsInt.xMin;
            int xMax = feature.BoundsInt.xMax;
            int yMin = feature.BoundsInt.yMin;
            int yMax = feature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    Cell cell = CurrentZoneInfo.grid[position];
                    if (cell.Contains<DownStairs>())
                        return position;
                }
            ExceptionHandler.Handle(new Exception("Could not check player path: down stairs have no been placed in zone."));
            return default;
        }
    }
}
