using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class CursorMovedEvent : Event
    {
        public override string Name => "Cursor Moved";

        public CursorMovedEvent() : base()
        {

        }
    }
}
