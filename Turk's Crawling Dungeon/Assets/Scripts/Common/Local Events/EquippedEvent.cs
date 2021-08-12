using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class EquippedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Equipped";

        public override string Id => id;

        ~EquippedEvent() => ReturnToPool();
    }
}
