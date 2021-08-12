using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class UnequippedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Unequipped";

        public override string Id => id;

        ~UnequippedEvent() => ReturnToPool();
    }
}
