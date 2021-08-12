using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Pathfinding;

namespace TCD.Zones
{
    public static class PlayerPathChecker
    {
        public static Vector2Int endRoomPosition = Vector2Int.zero;

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
            {
                IZone zone = CurrentZoneInfo.zone;
                IChamber finalChamber = zone.Chambers[zone.Chambers.Count - 1];
                endRoomPosition = FindEmptyPositionInChamber(finalChamber);    
            }
            return endRoomPosition;
        }

        private static Vector2Int FindEmptyPositionInChamber(IChamber chamber)
        {
            List<Vector2Int> emptyPositions = new List<Vector2Int>();
            int xMin = chamber.BoundsInt.xMin;
            int xMax = chamber.BoundsInt.xMax;
            int yMin = chamber.BoundsInt.yMin;
            int yMax = chamber.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (PositionChecker.IsEmpty(x, y) && PositionChecker.IsFloored(x, y))
                        emptyPositions.Add(new Vector2Int(x, y));
                }
            return Choose.Random(emptyPositions);
        }
    }
}
