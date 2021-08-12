using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public interface ICellObject 
    {
        Action<Cell> SetPositionEvent { get; set; }

        int X { get; set; }

        int Y { get; set; }

        Vector2Int Size { get; }

        Vector2Int Position { get; set; }

        Cell CurrentCell { get; }

        void SetPosition(Vector2Int position);

        void SetPosition(int x, int y);
    }
}
