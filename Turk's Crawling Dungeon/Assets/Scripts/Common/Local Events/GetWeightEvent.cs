using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetWeightEvent : LocalEvent
    {
        public float weight;

        public static new readonly string id = "Get Weight";

        public override string Id => id;

        ~GetWeightEvent() => ReturnToPool();

        protected override void Reset()
        {
            weight = 0f;
        }
    }
}

