using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class PlayerMovement : Movement
    {
        public override bool TryToMove(Vector2Int direction, bool isForced = false)
        {
            bool success = base.TryToMove(direction, isForced);
            if (success)
            {
                EventManager.Send(new PlayerMovedEvent());
                GameGrid grid = CurrentZoneInfo.grid;
                Cell cell = grid[Position];
                TimeScheduler.Tick(GetCostToMove() + GetMoveCostToCell(cell));
            }
            else
            {
                GameGrid grid = CurrentZoneInfo.grid;
                Cell cell = grid[Position + direction];
                if (TryGetObjectToAttack(cell, out BaseObject target) && parent.parts.TryGet(out Combat combat))
                    AttackHandler.AutoAttack(parent, target);
            }
            return success;
        }

        private bool TryGetObjectToAttack(Cell cell, out BaseObject target)
        {
            target = null;
            foreach (BaseObject obj in cell.objects)
            {
                if (obj.parts.Get<Combat>() && obj.faction != "Neutral" && obj.faction != "Player")
                {
                    target = obj;
                    return true;
                }
            }
            return false;
        }
    }
}
