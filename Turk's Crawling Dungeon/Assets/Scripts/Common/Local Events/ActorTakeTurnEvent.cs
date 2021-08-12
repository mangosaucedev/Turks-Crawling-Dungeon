using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ActorTakeTurnEvent : LocalEvent
    {
        public int energy;

        public static new readonly string id = "Actor Take Turn";

        public override string Id => id;

        ~ActorTakeTurnEvent() => ReturnToPool();

        protected override void Reset()
        {
            energy = 0;
        }
    }
}
