using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeHpModifiedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Hp Modified";

        public float amount;

        public override string Id => id;

        public bool IsDamage => amount < 0;

        public bool IsHeal => amount > 0;

        ~BeforeHpModifiedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            amount = 0f;
        }
    }
}
