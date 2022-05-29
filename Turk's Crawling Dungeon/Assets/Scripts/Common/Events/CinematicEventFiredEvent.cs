using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class CinematicEventFiredEvent : Event
    {
        public string e;

        public override string Name => "Cinematic Event";

        public CinematicEventFiredEvent(string e) : base()
        {
            this.e = e;
        }
    }
}
