using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class TGridEnumerator<T> : IEnumerator<T>
    {
        private TGrid<T> grid;
        private int x;
        private int y;

        private int Width => grid.width;

        private int Height => grid.height;

        public T Current => grid[x, y];

        object IEnumerator.Current => Current;

        public TGridEnumerator(TGrid<T> grid)
        {
            this.grid = grid;
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        { 
            x++;
            if (x == Width)
            {
                x = 0;
                y++;
            }
            if (y == Height)
                return false;
            return true;
        }

        public void Reset()
        {
            x = -1;
            y = 0;
        }
    }
}
