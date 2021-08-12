using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class GetMoveCostEvent : ObjectEvent
    {
        public BaseObject obj;
        public int cost;

        public static new readonly string id = "Get Move Cost";

        public override BaseObject Object => obj;

        public override string Id => id;

        ~GetMoveCostEvent() => ReturnToPool();

        protected override void Reset()
        {
            obj = null;
        }
    }
}
