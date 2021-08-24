using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetResourceRegenEvent : ActOnObjectEvent
    {
        public Resource resource;
        public float amount;

        public static new readonly string id = "Get Resource Regen";

        public override string Id => id;

        ~GetResourceRegenEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            resource = Resource.Hitpoints;
            amount = 0f;
        }
    }
}
