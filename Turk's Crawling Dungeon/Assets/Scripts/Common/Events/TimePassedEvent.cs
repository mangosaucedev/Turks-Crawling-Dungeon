using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class TimePassedEvent : Event
    {
        public int time;

        public override string Name => "Time Passed";

        public TimePassedEvent(int time) : base()
        {
            this.time = time;
        }
    }
}
