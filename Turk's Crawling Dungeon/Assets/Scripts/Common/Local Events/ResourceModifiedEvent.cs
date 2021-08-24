using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class ResourceModifiedEvent : ActOnObjectEvent
    {
        public Resource resource;
        public float amount;

        public static new readonly string id = "Resource Modified";

        public override string Id => id;

        ~ResourceModifiedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            resource = Resource.Hitpoints;
            amount = 0f;
        }
    }
}
