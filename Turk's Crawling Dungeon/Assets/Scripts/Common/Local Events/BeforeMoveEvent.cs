using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeMoveEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Move";

        public override string Id => id;

        ~BeforeMoveEvent() => ReturnToPool();
    }
}
