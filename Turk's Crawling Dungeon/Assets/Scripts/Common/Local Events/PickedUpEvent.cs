using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class PickedUpEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Picked Up";

        public override string Id => id;

        ~PickedUpEvent() => ReturnToPool();
    }
}
