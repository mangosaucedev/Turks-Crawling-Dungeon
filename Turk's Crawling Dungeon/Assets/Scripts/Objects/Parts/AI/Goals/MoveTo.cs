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

        protected override void OnInstantiation()
        {
            base.OnInstantiation();
            TryToFindPathToTarget();
        }

        public override int GetTimeCost()
        {
            if (path == null || !path.isValid)
            {
                FailToParent();
                return 0;
            }

            GetMoveCostEvent getMoveCostEvent = LocalEvent.Get<GetMoveCostEvent>();
            getMoveCostEvent.obj = brain.parent;
            getMoveCostEvent.cost = TimeInfo.TIME_PER_STANDARD_TURN;
            brain.FireEvent(brain.parent, getMoveCostEvent);
            int cost = getMoveCostEvent.cost;

            GetMoveCostToCellEvent getMoveCostToCellEvent = LocalEvent.Get<GetMoveCostToCellEvent>();
            getMoveCostToCellEvent.obj = brain.parent;
            Cell cell = GetNextCellInPath();
            if (cell == null)
                return 0;
            getMoveCostToCellEvent.cell = cell;
            getMoveCostToCellEvent.cost = cost;
            brain.FireEvent(cell, getMoveCostToCellEvent);
            if (getMoveCostToCellEvent.CanMoveToCell())
                return getMoveCostToCellEvent.cost;
            Think("Moving is too time-expensive!");
            return 0;
        }

        protected Cell GetNextCellInPath()
        {
            if (path == null || !path.isValid)
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

        public override void PerformAction()
        {
            base.PerformAction();

            Think("I am moving to a target position.");

            if (!TryToFindPathToTarget())
                return;

            if (Movement)
                MoveTowardsTargetPosition();
            else
                Think("I can't move!");
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