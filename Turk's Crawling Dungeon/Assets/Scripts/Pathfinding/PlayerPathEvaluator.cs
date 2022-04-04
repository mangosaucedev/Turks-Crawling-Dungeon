using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.Pathfinding
{
    public class PlayerPathEvaluator : INavEvaluator
    {
        public int GetDifficulty(NavNode node)
        {
            GameGrid gameGrid = CurrentZoneInfo.grid;
            Cell cell = gameGrid[node.position];
            int difficulty = 0;
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj.Parts.TryGet(out Obstacle obstacle))
                    difficulty += obstacle.Difficulty;
            }
            return difficulty;
        }

        public bool IsPassable(NavNode node)
        {
            GameGrid gameGrid = CurrentZoneInfo.grid;
            Cell cell = gameGrid[node.position];
            foreach (BaseObject obj in cell.Objects)
            {
                if (obj.Parts.TryGet(out Obstacle obstacle) && obstacle.IsImpassable && !obj.Parts.Get<Door>())
                    return false;
            }
            return true;
        }
    }
}
