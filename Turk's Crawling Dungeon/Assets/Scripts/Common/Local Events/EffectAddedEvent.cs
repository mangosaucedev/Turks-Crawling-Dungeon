using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD
{
    public class EffectAddedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Effect Added";

        public Effect effect;

        public override string Id => id;

        ~EffectAddedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            effect = null;
        }
    }
}
