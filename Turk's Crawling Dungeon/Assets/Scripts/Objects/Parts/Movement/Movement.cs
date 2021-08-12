using UnityEngine;

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
            Vector2Int newPosition = Position + direction;
            if (CurrentZoneInfo.grid.IsWithinBounds(newPosition))
            {
                if (!CanEnterCell(newPosition))
                    return FailMove();

                BeforeEnterCell(newPosition);
                EnterCell(newPosition);
                parent.cell.SetPosition(newPosition);

                //visualizer.StartVizualization(visualizer.MoveVisualizationRoutine());
                return true;
            }
            return FailMove();
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

        public void MoveSpriteToOrigin()
        {
            Transform spriteTransform = parent.SpriteRenderer.transform;
            spriteTransform.localPosition = Vector2.zero;
        }
    }
}
