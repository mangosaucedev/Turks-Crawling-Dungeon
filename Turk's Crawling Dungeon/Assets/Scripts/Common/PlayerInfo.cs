using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public static class PlayerInfo
    {
        public static BaseObject currentPlayer;
        
        public static int GetDistanceToPlayer(BaseObject obj)
        {
            Vector2Int position = obj.cell.Position;
            Vector2Int playerPosition = currentPlayer.cell.Position;
            return Mathf.FloorToInt(Vector2Int.Distance(position, playerPosition));
        }
    }
}
