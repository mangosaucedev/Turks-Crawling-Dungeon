using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class DroppedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Dropped";

        public override string Id => id;

        ~DroppedEvent() => ReturnToPool();
    }
}
