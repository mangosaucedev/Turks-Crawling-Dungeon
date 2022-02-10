using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class TGrid<T> : IEnumerable<T>
    {
        public int width;
        public int height;
        public T[,] cells;

        public T this[Vector2Int position]
        {
            get => Get(position);
            set => Set(position, value);
        }

        public T this[int x, int y]
        {
            get => Get(x, y);
            set => Set(x, y, value);
        }

        public TGrid(int width, int height)
        {
            if (width <= 0)
                ExceptionHandler.Handle(new Exception(
                    $"Tried to make grid with invalid width ({width})!"));
            if (height <= 0)
                ExceptionHandler.Handle(new Exception(
                    $"Tried to make grid with invalid height ({height})!"));
            this.width = width;
            this.height = height;
            cells = new T[width, height];
            FillWithDefault();
        }

        protected void FillWithDefault()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Set(x, y, GetDefault());
        }

        protected virtual T GetDefault() => default;

        public bool IsWithinBounds(Vector2Int position) =>
            IsWithinBounds(position.x, position.y);

        public bool IsWithinBounds(int x, int y) =>
            (x >= 0 && x < width && y >= 0 && y < height);

        public bool IsOnEdge(Vector2Int position) =>
            IsOnEdge(position.x, position.y);

        public bool IsOnEdge(int x, int y) =>
            (x == 0 || x == width - 1 || y == 0 || y == height - 1);

        public T Get(Vector2Int position) =>
            Get(position.x, position.y);

        public T Get(int x, int y)
        {
            if (IsWithinBounds(x, y))
                return cells[x, y];
            ThrowIndexOutOfRangeException(x, y);
            return default;
        }

        public void Set(Vector2Int position, T value) =>
            Set(position.x, position.y, value);

        public void Set(int x, int y, T value)
        {
            if (IsWithinBounds(x, y))
                cells[x, y] = value;
            else
                ThrowIndexOutOfRangeException(x, y);
        }

        private void ThrowIndexOutOfRangeException(int x, int y) =>
            ExceptionHandler.Handle(new Exception(
                $"Index {x}, {y} out of bounds of grid ({width}, {height})!"));

        protected void CopyFrom(TGrid<T> other)
        {
            width = other.width;
            height = other.height;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    T value = other[x, y];
                    Set(x, y, value);
                }
        }

        public TGrid<T> Clone()
        {
            TGrid<T> newGrid = new TGrid<T>(width, height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    newGrid[x, y] = this[x, y];
                }

            return newGrid;
        }

        public IEnumerator<T> GetEnumerator() => new TGridEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
}