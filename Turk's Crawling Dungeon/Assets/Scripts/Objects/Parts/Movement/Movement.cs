using UnityEngine;
using TCD.Pathfinding;

namespace TCD.Objects.Parts
{
    public class Movement : Part
    { 
        public const int MIN_MOVE_TIME = 40;

        public Vector2Int movementVector;
        private MovementVisualizer visualizer;

        public override string Name => "Movement";

        protected override void Awake()
        {
            base.Awake();
            visualizer = new MovementVisualizer(this);
        }

        public virtual bool TryToMove(Vector2Int direction, bool isForced = false)
        {
            movementVector = direction;
            MoveSpriteToOrigin();

            if (!CanMoveAndExitCurrentCell())
                return false;

            Vector2Int newPosition = Position + direction;
            if (CurrentZoneInfo.grid.IsWithinBounds(newPosition))
            {
                if (!CanEnterCell(newPosition))
                    return FailMove();

                ExitCell(Position);
                SetPosition(newPosition);

                //visualizer.StartVizualization(visualizer.MoveVisualizationRoutine());
                return true;
            }
            return FailMove();
        }


        public bool CanMoveAndExitCurrentCell() => CanMove() && CanExitCurrentCell();
        
        private bool CanMove()
        {
            BeforeMoveEvent e = LocalEvent.Get<BeforeMoveEvent>();
            e.obj = parent;
            return FireEvent(parent, e);
        }

        private bool CanExitCurrentCell()
        {
            CanExitCellEvent e = LocalEvent.Get<CanExitCellEvent>();
            e.obj = parent;
            e.cell = parent.cell.CurrentCell;
            return FireEvent(e.cell, e);
        }

        private bool CanEnterCell(Vector2Int position)
        {
            Cell cell = CurrentZoneInfo.grid[position];
            CanEnterCellEvent e = LocalEvent.Get<CanEnterCellEvent>();
            e.cell = cell;
            bool isSuccessful = FireEvent(cell, e);
            bool canEnterCell = e.CanEnterCell();
            if (isSuccessful && !canEnterCell)
                MessageLog.Add("Ouch!");
            return isSuccessful && canEnterCell;
        }

        private bool FailMove()
        {
            //visualizer.StartVizualization(visualizer.MoveFailedVizualizationRoutine());
            return false;
        }

        public void SetPosition(Vector2Int position)
        {
            BeforeEnterCell(position);
            EnterCell(position);
            parent.cell.SetPosition(position);
        }

        private void BeforeEnterCell(Vector2Int position)
        {
            Cell cell = CurrentZoneInfo.grid[position];
            BeforeEnterCellEvent e = LocalEvent.Get<BeforeEnterCellEvent>();
            e.cell = cell;
            FireEvent(cell, e);
        }

        public void EnterCell(Vector2Int position)
        {
            Cell cell = CurrentZoneInfo.grid[position];
            EnteredCellEvent e = LocalEvent.Get<EnteredCellEvent>();
            e.obj = parent;
            e.cell = cell;
            FireEvent(cell, e);
        }

        public void ExitCell(Vector2Int position)
        {
            Cell cell = CurrentZoneInfo.grid[position];
            ExitedCellEvent e = LocalEvent.Get<ExitedCellEvent>();
            e.obj = parent;
            e.cell = cell;
            FireEvent(cell, e);
        }

        public void MoveSpriteToOrigin()
        {
            Transform spriteTransform = parent.SpriteRenderer.transform;
            spriteTransform.localPosition = Vector2.zero;
        }

        public int GetCostToMove()
        {
            if (parent.parts.TryGet(out Stats stats))
                return stats.GetStatLevel(Stat.MoveCost);
            return TimeInfo.TIME_PER_STANDARD_TURN;
        }

        public int GetMoveCostToCell(Cell cell)
        {
            GetMoveCostToCellEvent getMoveCostToCellEvent = LocalEvent.Get<GetMoveCostToCellEvent>();
            getMoveCostToCellEvent.obj = parent;
            getMoveCostToCellEvent.cell = cell;
            FireEvent(cell, getMoveCostToCellEvent);
            if (getMoveCostToCellEvent.CanMoveToCell())
                return getMoveCostToCellEvent.cost;
            return 999;
        }

        public virtual bool TryMoveToPosition(Vector2Int targetPosition)
        {
            GridRay ray = GridRaycaster.Raycast(Position, targetPosition, new BlockedByObstaclesEvaluator()).ray;
            for (int i = 1; i < ray.positions.Count; i++)
            { 
                Vector2Int nextPosition = ray.positions[i];
                Vector2Int unclampedDirection = nextPosition - Position;
                float roundedX = Mathf.Round(unclampedDirection.x);
                float roundedY = Mathf.Round(unclampedDirection.y);
                int xDirection = (int)Mathf.Clamp(roundedX, -1, 1);
                int yDirection = (int)Mathf.Clamp(roundedY, -1, 1);
                movementVector = new Vector2Int(xDirection, yDirection);
                if (CanMoveAndExitCurrentCell())
                    SetPosition(nextPosition);
                if (Position == targetPosition)
                    return true;
            }
            return false;
        }

        public bool TryToNavigatePath(NavAstarPath path)
        {
            if (!path.isValid || path.path.Count == 0)
                return false;
            for (int i = 0; i < path.path.Count; i++)
            {
                Vector2Int nextPosition = path.path[i];
                Vector2Int unclampedDirection = nextPosition - Position;
                float roundedX = Mathf.Round(unclampedDirection.x);
                float roundedY = Mathf.Round(unclampedDirection.y);
                int xDirection = (int)Mathf.Clamp(roundedX, -1, 1);
                int yDirection = (int)Mathf.Clamp(roundedY, -1, 1);
                movementVector = new Vector2Int(xDirection, yDirection);
                if (!TryToMove(movementVector))
                {
                    DebugLogger.Log("Could not move in direction " + movementVector);
                    return false;
                }
            }
            return true;
        }
    }
}
