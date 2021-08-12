using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class GetHpEvent : ActOnObjectEvent
    {
        public float hp;

        public static new readonly string id = "Get Hp";

        public override string Id => id;

        ~GetHpEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            hp = 0f;
        }
    }
}
