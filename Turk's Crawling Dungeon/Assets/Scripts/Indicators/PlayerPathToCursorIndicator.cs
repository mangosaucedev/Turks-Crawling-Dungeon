using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Pathfinding;
using TCD.Zones;

namespace TCD.Indicators
{
    public class PlayerPathToCursorIndicator : Indicator
    {
        public override void Start()
        {
            MouseCursor mouseCursor = ServiceLocator.Get<MouseCursor>();
            if (!PlayerInfo.currentPlayer)
                IndicatorHandler.HideIndicator();
            startPosition = PlayerInfo.currentPlayer.cell.Position;
            targetPosition = mouseCursor.GetGridPosition();
            base.Start();
        }

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
            if (!CurrentZoneInfo.grid.IsWithinBounds(targetPosition) || !PositionChecker.IsFloored(targetPosition))
                return;
            NavAstarPath path = new NavAstarPath(startPosition, targetPosition, new PlayerPathEvaluator());
            if (path.isValid)
            {
                bool isBlocked = false;
                for (int i = 0; i < path.path.Count; i++)
                {
                    Vector2Int position = path.path[i];
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
            else
            {
                GridRay ray = GridRaycaster.Raycast(startPosition, targetPosition, new BlockedByObstaclesEvaluator()).ray;
                Sprite sprite = Assets.Get<Sprite>("IndicatorInvalid");
                for (int i = 0; i < ray.positions.Count; i++)
                    IndicateTile(ray.positions[i], sprite);
            }
        }
    }
}
