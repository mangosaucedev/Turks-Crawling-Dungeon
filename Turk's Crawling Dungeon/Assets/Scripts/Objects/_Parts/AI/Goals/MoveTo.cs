using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Pathfinding;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class MoveTo : Goal
    {
        public NavAstarPath path;

        private Vector2Int targetPosition;

        protected virtual Vector2Int TargetPosition => targetPosition;

        protected Movement Movement => brain.parent.parts.Get<Movement>();

        public MoveTo(Brain brain, Vector2Int targetPosition) : 
            base(brain)
        {
            this.targetPosition = targetPosition;
        }

        public override int GetTimeCost()
        {
            if (path == null || path.GetTargetPosition() != GetTargetPosition())
                path = new NavAstarPath(Position, GetTargetPosition(), new ObjectPathEvaluator());
                
            if (!path.isValid)
            {
                FailToParent();
                return 0;
            }

            Cell cell = GetNextCellInPath();
            if (cell == null)
                return 0;
            return Movement.GetCostToMove() + Movement.GetMoveCostToCell(cell);
        }

        protected Cell GetNextCellInPath()
        {
            if ((path == null || path.GetTargetPosition() != GetTargetPosition()) && !TryToFindPathToTarget())
                return null;

            Vector2Int nextPosition = Position;

            if (path.TryToGetIndexOfPosition(Position, out int i) && path.path.Count > i)
                nextPosition = path.path[i + 1];
            else if (path.path.Count > 0)
                nextPosition = path.path[0];

            if (nextPosition == Position)
                return null;

            return CurrentZoneInfo.grid[nextPosition];
        }

        public override bool PerformAction()
        {
            if (!base.PerformAction())
                return false;

            Think("I am moving to a target position.");

            if ((path == null || path.GetTargetPosition() != GetTargetPosition()) && !TryToFindPathToTarget())
                return false;

            if (Movement)
            {
                MoveTowardsTargetPosition();
                return true;
            }
            else
                Think("I can't move!");
            return false;
        }

        protected virtual Vector2Int GetTargetPosition() =>
            targetPosition;

        private bool TryToFindPathToTarget()
        {
            path = new NavAstarPath(brain.Position, GetTargetPosition(), new ObjectPathEvaluator());
            if (!path.isValid)
            {
                Think("Could not path to target!");
                FailToParent();
                return false;
            }
            Think($"I've successfully calculated a path to my target in {path.stepsToCreate} steps.");
            return true;
        }

        protected virtual void MoveTowardsTargetPosition()
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
            Movement.TryToMove(direction);
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
                return true;
            return false;
        }
    }
}