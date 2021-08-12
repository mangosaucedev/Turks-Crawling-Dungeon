using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GetHpMaxEvent : ActOnObjectEvent
    {
        public float hpMax;

        public static new readonly string id = "Get Max Hp";

        public override string Id => id;

        ~GetHpMaxEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            hpMax = 0f;
        }
    }
}
