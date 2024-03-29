using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class PlayerMovement : Movement
    {
        public override bool TryToMove(Vector2Int direction, bool isForced = false, bool isScheduled = false)
        {
            bool success = base.TryToMove(direction, isForced, isScheduled);
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
                if (TryGetObjectToAttack(cell, out BaseObject target) && parent.Parts.TryGet(out Combat combat))
                {
                    AttackHandler.AutoAttack(parent, target);
                    TimeScheduler.Tick(combat.GetAttackCost(target));
                }
                else
                    FailMove();
            }
            return success;
        }

        private bool TryGetObjectToAttack(Cell cell, out BaseObject target)
        {
            target = null;
            foreach (BaseObject obj in cell.Objects)
            {
                Brain brain = obj.Parts.Get<Brain>();
                if (obj.Parts.Get<Combat>() && brain?.Faction != "Neutral" && brain?.Faction != "Player")
                {
                    target = obj;
                    return true;
                }
            }
            return false;
        }
    }
}
