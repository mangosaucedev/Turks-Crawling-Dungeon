using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO.Serialization
{
    [Serializable]
    public class SerializableVector2Int 
    {
        public int x;
        public int y;

        public SerializableVector2Int(Vector2Int position)
        {
            x = position.x;
            y = position.y;
        }
    }
}
