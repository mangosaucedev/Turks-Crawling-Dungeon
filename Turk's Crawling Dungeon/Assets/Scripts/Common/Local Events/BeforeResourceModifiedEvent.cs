using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class BeforeResourceModifiedEvent : ActOnObjectEvent
    {
        public Resource resource;
        public float amount;

        public static new readonly string id = "Before Resource Modified";

        public override string Id => id;

        ~BeforeResourceModifiedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            resource = Resource.Hitpoints;
            amount = 0f;
        }
    }
}
