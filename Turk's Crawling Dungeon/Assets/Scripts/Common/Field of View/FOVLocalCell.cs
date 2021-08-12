using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class FOVLocalCell 
    {
        public Vector2Int localPosition;

        public int Depth
        {
            get
            {
                return localPosition.y;
            }
        }

        public int Position
        {
            get
            {
                return localPosition.x;
            }
        }

        public FOVLocalCell(Vector2Int _localPosition)
        {
            localPosition = _localPosition;
        }
    }
}