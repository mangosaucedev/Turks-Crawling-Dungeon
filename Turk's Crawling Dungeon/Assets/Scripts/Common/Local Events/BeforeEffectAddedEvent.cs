using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD
{
    public class BeforeEffectAddedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Effect Added";

        public Effect effect;

        public override string Id => id;

        ~BeforeEffectAddedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            effect = null;
        }
    }
}
