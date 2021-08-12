using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;

namespace TCD
{
    public class AttackedEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Attacked";

        public BaseObject attacker;
        public Attack attack;
        public float damage;

        public override string Id => id;

        ~AttackedEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            attacker = null;
            attack = null;
            damage = 0;
        }
    }
}
