using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;

namespace TCD
{
    public class GetAttacksEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Get Attacks";

        public BaseObject target;

        private GrabBag<Attack> attacks = new GrabBag<Attack>();

        public override string Id => id;

        ~GetAttacksEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            target = null;
            attacks.Reset();
        }

        public void AddAttack(Attack attack, float weight)
        {
            float averageDamage = attack.GetAverageDamage();
            attacks.AddItem(attack, averageDamage * weight);
        }

        public Attack GetAttack() => attacks.Grab();
    }
}
