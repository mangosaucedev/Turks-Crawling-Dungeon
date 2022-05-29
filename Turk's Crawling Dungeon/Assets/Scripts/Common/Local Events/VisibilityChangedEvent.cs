using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class VisibilityChangedEvent : LocalEvent
    {
        public static new readonly string id = "Visibility Changed";

        public bool visible;

        public override string Id => id;

        ~VisibilityChangedEvent() => ReturnToPool();

        protected override void Reset()
        {
            visible = false;
        }
    }
}
