using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetInteractionsEvent : ActOnObjectEvent
    {
        public List<Interaction> interactions = new List<Interaction>();

        public static new readonly string id = "Get Interactions";

        public override string Id => id;

        ~GetInteractionsEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            interactions.Clear();
        }
    }
}

