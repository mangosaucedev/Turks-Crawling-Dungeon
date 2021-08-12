using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeTurnTickEvent : Event
    {
        public int timeElapsed;

        public override string Name => "Before Turn Tick";
    
        public BeforeTurnTickEvent(int timeElapsed) : base()
        {
            this.timeElapsed = timeElapsed;
        }
    }
}
