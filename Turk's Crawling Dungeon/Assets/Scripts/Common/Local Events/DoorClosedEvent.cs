using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class DoorClosedEvent : LocalEvent
    {
        public static new readonly string id = "Door Closed";

        public override string Id => id;

        ~DoorClosedEvent() => ReturnToPool();

        protected override void Reset()
        {

        }
    }
}
