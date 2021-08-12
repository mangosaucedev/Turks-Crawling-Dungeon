using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class PlayerCreatedEvent : Event
    {
        public override string Name => "Player Created";

        public PlayerCreatedEvent() : base()
        {

        }
    }
}
