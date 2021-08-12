using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class HpModifiedEvent : ActOnObjectEvent
    {
        public float amount;

        public static new readonly string id = "Hp Modified";

        public override string Id => id;

        public bool IsDamage => amount < 0;

        public bool IsHeal => amount > 0;

        ~HpModifiedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            amount = 0f;
        }
    }
}
