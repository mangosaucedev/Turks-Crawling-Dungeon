using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    [Serializable]
    public class CellObject : ObjectComponent, ICellObject
    {
        private int x;
        private int y;
        private Cell currentCell;
        private Action<Cell> setPositionEvent = c => { };

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public Vector2Int Size => new Vector2Int(1, 1);

        public Vector2Int Position
        {
            get => new Vector2Int(X, Y);
            set => SetPosition(value);
        }

        public Cell CurrentCell => currentCell;

        public Action<Cell> SetPositionEvent
        {
            get => setPositionEvent;
            set => setPositionEvent = value;
        }

        public CellObject(BaseObject parent) : base(parent)
        {

        }

        public void SetPosition(Vector2Int position) => 
            SetPosition(position.x, position.y);

        public void SetPosition(int x, int y)
        {
            if (CurrentCell != null)
                CurrentCell.Remove(parent);
            GameGrid grid = CurrentZoneInfo.grid;
            if (grid == null)
                return;
            X = x;
            Y = y;
            Cell cell = grid[x, y];
            cell.Add(parent);
            currentCell = cell;
            SetParentPositionToCurrentCell();
            SetPositionEvent?.Invoke(cell);
        }

        private void SetParentPositionToCurrentCell()
        {
            int newX = currentCell.X;
            int newY = currentCell.Y;
            int realX = newX * Cell.SIZE;
            int realY = newY * Cell.SIZE;
            realX += Mathf.FloorToInt(Size.x * Cell.SIZE / 2);
            realY += Mathf.FloorToInt(Size.y * Cell.SIZE / 2);
            if (parent)
                parent.transform.position = new Vector3(realX, realY);
        }
    }
}
