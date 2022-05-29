using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TCD.Pathfinding;
using TCD.Threading;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class MoveTo : Goal
    {
        public AstarPath nativePath;

        private Vector2Int targetPosition;
        private ObjectPathEvaluator evaluator = new ObjectPathEvaluator();
        private NavAstarPath path;

        public Movement Movement => brain.parent.Parts.Get<Movement>();

        public virtual Vector2Int TargetPosition => targetPosition;

        public MoveTo(Brain brain, Vector2Int targetPosition) : 
            base(brain)
        {
            this.targetPosition = targetPosition;
        }

        public override int GetTimeCost()
        {
            if (!TryGetPath())    
                return 0;

            Cell cell = GetNextCellInPath();
            if (cell == null)
                return 0;
            return Movement.GetCostToMove() + Movement.GetMoveCostToCell(cell);
        }

        protected Cell GetNextCellInPath()
        {
            if (!TryGetPath())
            {
                Think("Could not get next cell in path: invalid path!");
                FailToParent();
                return null;
            }

            Vector2Int nextPosition = Position;

            if (path.TryToGetIndexOfPosition(Position, out int i) && (path.path.Count > i))
                nextPosition = path.path[i + 1];
            else if (path.path.Count > 0)
                nextPosition = path.path[0];

            if (nextPosition == Position)
            {
                Think("Could not get next cell in path: could not find next position in path!");
                return null;
            }

            return CurrentZoneInfo.grid[nextPosition];
        }

        public override bool PerformAction()
        {
            if (!base.PerformAction())
                return false;

            if (!Movement)
            {
                Think("I can't move!");
                FailToParent();
                return false;
            }
            
            bool gotPath = TryGetPath();
            if (gotPath)
                ActionScheduler.EnqueueAction(id, MoveTowardsTargetPosition, ActionType.Goal);
            else
            {
                Think("No valid path to target!");
                FailToParent();
                return false;
            }
            return true;
        }

        protected virtual Vector2Int GetTargetPosition() =>
            targetPosition;

        private bool TryGetPath()
        {
            if (path == null || path.GetTargetPosition() != GetTargetPosition())
            {
                path = new NavAstarPath(brain.Position, GetTargetPosition(), evaluator);
                return path.isValid;
            }
            return true;
        }

        public virtual void MoveTowardsTargetPosition()
        {
            Cell cell = GetNextCellInPath();

            if (cell == null)
                return;

            AIBeforeMoveEvent e = LocalEvent.Get<AIBeforeMoveEvent>();
            e.obj = obj;
            e.nextCell = cell;
            e.targetPosition = GetTargetPosition();
            if (!obj.HandleEvent(e))
                return;

            Vector2Int direction = GetDirectionToCell(cell);
            Think($"I am moving towards my target in direction: {direction}.");
            Movement.TryToMove(direction, false, true);
        }

        private Vector2Int GetDirectionToCell(Cell cell)
        {
            Vector2Int cellPosition = cell.Position;
            Vector2 unclampedDirection = cellPosition - Position;
            float roundedX = Mathf.Round(unclampedDirection.x);
            float roundedY = Mathf.Round(unclampedDirection.y);
            int xDirection = (int) Mathf.Clamp(roundedX, -1, 1);
            int yDirection = (int) Mathf.Clamp(roundedY, -1, 1);
            return new Vector2Int(xDirection, yDirection);
        }

        public override bool IsFinished()
        {
            if (!Movement)
            {
                FailToParent();
                Think("Move job finished cause I can't move!");
                return true;
            }
            return false;
        }
    }
}