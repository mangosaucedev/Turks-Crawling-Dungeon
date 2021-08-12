using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class CursorStateChangeEvent : Event
    {
        public CursorState state;

        public override string Name => "Cursor State Change";

        public CursorStateChangeEvent(CursorState state) : base()
        {
            this.state = state;
        }
    }
}
