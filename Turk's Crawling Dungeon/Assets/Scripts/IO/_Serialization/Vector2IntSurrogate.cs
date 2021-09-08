using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO.Serialization
{
    [Serializable]
    public class Vector2IntSurrogate 
    {
        public int x;
        public int y;

        public Vector2IntSurrogate(Vector2Int position)
        {
            x = position.x;
            y = position.y;
        }
    }
}
