using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GridRay
    {
        public Vector2Int startPosition;
        public Vector2Int endPosition;

        public List<Vector2Int> positions = new List<Vector2Int>();
        private int x1;
        private int x2;
        private int y1;
        private int y2;
        bool steep;
        bool reverse;

        public GridRay(Vector2Int startPosition, Vector2Int endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            PlotRay();
            if (reverse)
                positions.Reverse();
        }

        private void PlotRay()
        {
            Initialize();
            int deltaX = x2 - x1;
            int deltaY = Mathf.Abs(y2 - y1);
            int error = 0;
            int ystep = (y1 < y2) ? 1 : -1;
            int y = y1;
            for (int x = x1; x <= x2; x++)
            {
                if (steep && !positions.Contains(new Vector2Int(y, x)))
                    positions.Add(new Vector2Int(y, x));
                else if (!positions.Contains(new Vector2Int(x, y)))
                    positions.Add(new Vector2Int(x, y));
                error += deltaY;
                if (2 * error >= deltaX)
                {
                    y += ystep;
                    error -= deltaX;
                }
            }

        }

        private void Swap<T>(ref T a, ref T b)
        {
            T aStored = a;
            a = b;
            b = aStored;
        }

        private void Initialize()
        {
            x1 = startPosition.x;
            x2 = endPosition.x;
            y1 = startPosition.y;
            y2 = endPosition.y;
            steep = Mathf.Abs(y2 - y1) > Mathf.Abs(x2 - x1);
            if (steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
                reverse = true;
            }
        }
    }
}
