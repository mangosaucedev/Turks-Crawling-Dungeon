using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD
{
    public class EffectRemovedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Effect Removed";

        public Effect effect;

        public override string Id => id;

        ~EffectRemovedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            effect = null;
        }
    }
}
