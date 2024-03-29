using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD
{
    public static class PlayerInfo
    {
        public static BaseObject currentPlayer;
        public static Class currentClass;
        public static int talentPoints;

        public static int GetDistanceToPlayer(BaseObject obj)
        {
            Vector2Int position = obj.cell.Position;
            Vector2Int playerPosition = currentPlayer.cell.Position;
            return Mathf.FloorToInt(Vector2Int.Distance(position, playerPosition));
        }

        public static int GetDistanceToPlayer(Vector3Int position)
        {
            Vector2Int playerPosition = currentPlayer.cell.Position;
            return Mathf.FloorToInt(Vector2Int.Distance((Vector2Int) position, playerPosition));
        }

        public static int GetDistanceToPlayer(Vector2Int position)
        {
            Vector2Int playerPosition = currentPlayer.cell.Position;
            return Mathf.FloorToInt(Vector2Int.Distance(position, playerPosition));
        }

        public static int GetTier() => Mathf.FloorToInt(ScoreHandler.level / 3f);
    }
}
