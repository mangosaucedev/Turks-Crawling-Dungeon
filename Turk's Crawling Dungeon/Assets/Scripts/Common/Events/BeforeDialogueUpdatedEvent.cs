using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeDialogueUpdatedEvent : Event
    {
        public override string Name => "Before Dialogue Updated";

        public BeforeDialogueUpdatedEvent() : base()
        {

        }
    }
}
