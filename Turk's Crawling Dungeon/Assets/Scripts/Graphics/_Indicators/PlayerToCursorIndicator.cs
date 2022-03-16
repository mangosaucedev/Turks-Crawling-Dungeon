using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Graphics.Indicators
{
    public class PlayerToCursorIndicator : Indicator
    {
        protected override void OnMouseEnterCell(MouseEnterCellEvent e)
        {
            startPosition = PlayerInfo.currentPlayer.cell.Position;
            targetPosition = e.cell.Position;
            base.OnMouseEnterCell(e);
        }

        protected override void OnCursorMoved(CursorMovedEvent e)
        {
            startPosition = PlayerInfo.currentPlayer.cell.Position;
            targetPosition = e.position;
            base.OnCursorMoved(e);
        }

        protected override void UpdateIndicator()
        {
            base.UpdateIndicator();
            GridRaycastEvaluator evaluator = (blockedByObstacles) ? (GridRaycastEvaluator) new BlockedByObstaclesEvaluator() : new FreeEvaluator();
            GridRaycastResult result = GridRaycaster.Raycast(startPosition, targetPosition, evaluator);
            GridRay ray = result.ray;
            bool isBlocked = false;
            for (int i = 0; i < ray.positions.Count; i++)
            {
                Vector2Int position = ray.positions[i];
                int distance = Mathf.FloorToInt(Vector2Int.Distance(position, startPosition));
                Sprite sprite = Assets.Get<Sprite>("IndicatorValid");
                if (i > 0)
                {
                    bool isVisible = FieldOfView.IsVisible(position);
                    if (!isBlocked && isVisible)
                    {
                        bool positionBlockedByObstacle = (blockedByObstacles &&
                            CurrentZoneInfo.grid.IsWithinBounds(position) &&
                            CurrentZoneInfo.grid[position].Contains(out Obstacle obstacle) &&
                            obstacle.IsImpassable);
                        if (distance > range || positionBlockedByObstacle)
                            isBlocked = true;
                    }
                    if (!isVisible)
                        sprite = Assets.Get<Sprite>("IndicatorUnknown");
                    if (isBlocked)
                        sprite = Assets.Get<Sprite>("IndicatorInvalid");
                }
                IndicateTile(position, sprite);
            }
        }
    }
}
