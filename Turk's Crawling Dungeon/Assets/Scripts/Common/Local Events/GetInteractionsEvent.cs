using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetInteractionsEvent : LocalEvent
    {
        public List<Interaction> interactions = new List<Interaction>();

        public static new readonly string id = "Get Move Cost";

        public override string Id => id;

        ~GetInteractionsEvent() => ReturnToPool();

        protected override void Reset()
        {
            interactions.Clear();
        }
    }
}

