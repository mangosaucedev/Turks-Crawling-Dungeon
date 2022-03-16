using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MouseCursor : MonoBehaviour
    {
        public Vector2Int currentPosition;
        public Vector2Int previousPosition;
        
        public event Action onMouseMoved;

        private void LateUpdate()
        {
            if (CurrentZoneInfo.grid != null)
                CalculateCurrentPosition();
        }

        private void CalculateCurrentPosition()
        {
            previousPosition = currentPosition;
            currentPosition = GetGridPosition();

            if (currentPosition != previousPosition)
            {
                GameGrid grid = CurrentZoneInfo.grid;
                if (grid.IsWithinBounds(previousPosition))
                {
                    Cell cell = grid[previousPosition];
                    EventManager.Send(new MouseExitCellEvent(cell));
                } 
                if (grid.IsWithinBounds(currentPosition))
                {
                    Cell cell = grid[currentPosition];
                    EventManager.Send(new MouseEnterCellEvent(cell));
                }
                onMouseMoved?.Invoke();
            }
        }

        public Vector2Int GetGridPosition()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(worldPosition.x / Cell.SIZE);
            int y = Mathf.FloorToInt(worldPosition.y / Cell.SIZE);
            return new Vector2Int(x, y);
        }

        public Cell GetCell()
        {
            GameGrid grid = CurrentZoneInfo.grid;
            Vector2Int position = GetGridPosition();
            int x = Mathf.Clamp(position.x, 0, grid.width - 1);
            int y = Mathf.Clamp(position.y, 0, grid.height - 1);
            return grid[x, y];
        }
    }
}
