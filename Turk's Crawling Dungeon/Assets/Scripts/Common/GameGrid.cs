using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GameGrid : TGrid<Cell>
    {
        private static GameGrid current;

        public static GameGrid Current
        {
            get
            {
                if (current == null)
                    current = CurrentZoneInfo.grid;
                return current;
            }
        }

        public GameGrid(int width, int height) : base(width, height)
        {
            FillWithCells();
        }

        public static Vector3 GridToWorld(Vector2Int vector)
        {
            return GridToWorld(vector.x, vector.y);
        }

        public static Vector3 GridToWorld(int x, int y)
        {
            return new Vector3(x * Cell.SIZE, y * Cell.SIZE) + new Vector3(Cell.SIZE / 2, Cell.SIZE / 2);
        }

        public static Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            float x = worldPosition.x;
            float y = worldPosition.y;
            Vector2Int vector = new Vector2Int(Mathf.FloorToInt(x / Cell.SIZE), Mathf.FloorToInt(y / Cell.SIZE));
            vector.x = Mathf.Clamp(vector.x, 0, Current.width);
            vector.y = Mathf.Clamp(vector.y, 0, Current.height);
            return vector;
        }

        public static bool IsWorldPositionInGrid(Vector3 worldPosition)
        {
            float x = worldPosition.x;
            float y = worldPosition.y;
            Vector2Int vector = new Vector2Int(Mathf.FloorToInt(x / Cell.SIZE), Mathf.FloorToInt(y / Cell.SIZE));
            return !(vector.x < 0 || vector.x > Current.width || vector.y < 0 || vector.y > Current.height);
        }

        private void FillWithCells()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Cell cell = new Cell(x, y);
                    Set(x, y, cell);
                }
        }
    }
}
