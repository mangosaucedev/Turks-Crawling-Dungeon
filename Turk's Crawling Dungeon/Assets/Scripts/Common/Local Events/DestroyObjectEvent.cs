using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class DestroyObjectEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Destroy Object";

        public override string Id => id;

        ~DestroyObjectEvent() => ReturnToPool();
    }
}
