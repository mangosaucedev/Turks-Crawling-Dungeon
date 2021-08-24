using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class AIBeforeAttackEvent : AICommandEvent
    {
        public static new readonly string id = "AI Before Attack";

        public BaseObject target;

        public override string Id => id;

        ~AIBeforeAttackEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            target = null;
        }
    }
}
