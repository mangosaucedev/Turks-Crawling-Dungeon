using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AfterTurnTickEvent : Event
    {
        public int timeElapsed;

        public override string Name => "After Turn Tick";

        public AfterTurnTickEvent(int timeElapsed) : base()
        {
            this.timeElapsed = timeElapsed;
        }
    }
}
