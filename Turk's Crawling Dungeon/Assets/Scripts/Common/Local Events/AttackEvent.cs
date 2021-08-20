using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;

namespace TCD
{
    public class AttackEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Attack";

        public BaseObject defender;
        public Attack attack;
        public float damage;

        public override string Id => id;

        ~AttackEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            defender = null;
            attack = null;
            damage = 0;
        }
    }
}