using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD
{
    public class BeforeEffectRemovedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Effect Removed";

        public Effect effect;

        public override string Id => id;

        ~BeforeEffectRemovedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            effect = null;
        }
    }
}
