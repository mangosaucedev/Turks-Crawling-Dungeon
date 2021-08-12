using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetMaxWeightEvent : LocalEvent
    {
        public float maxWeight;

        public static new readonly string id = "Get Max Weight";

        public override string Id => id;

        ~GetMaxWeightEvent() => ReturnToPool();

        protected override void Reset()
        {
            maxWeight = 0f;
        }
    }
}

