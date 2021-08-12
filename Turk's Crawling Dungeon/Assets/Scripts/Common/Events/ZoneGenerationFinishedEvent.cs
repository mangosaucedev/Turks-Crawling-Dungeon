using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ZoneGenerationFinishedEvent : Event
    {
        public override string Name => "Zone Generation Finished";

        public ZoneGenerationFinishedEvent() : base()
        {

        }
    }
}
