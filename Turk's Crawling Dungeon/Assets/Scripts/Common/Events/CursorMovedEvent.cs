using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class CursorMovedEvent : Event
    {
        public Vector2Int position;

        public override string Name => "Cursor Moved";

        public CursorMovedEvent(Vector2Int position) : base()
        {
            this.position = position;
        }
    }
}
