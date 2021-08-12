using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeDestroyObjectEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Destroy Object";

        public override string Id => id;

        ~BeforeDestroyObjectEvent() => ReturnToPool();
    }
}
