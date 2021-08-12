using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class DoorOpenedEvent : LocalEvent
    {
        public static new readonly string id = "Door Opened";

        public override string Id => id;

        ~DoorOpenedEvent() => ReturnToPool();

        protected override void Reset()
        {
            
        }
    }
}
