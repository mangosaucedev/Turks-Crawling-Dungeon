using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeInspectEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Inspect";

        public override string Id => id;

        ~BeforeInspectEvent() => ReturnToPool();
    }
}
