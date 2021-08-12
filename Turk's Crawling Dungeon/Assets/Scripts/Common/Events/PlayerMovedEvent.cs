using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class PlayerMovedEvent : Event
    {
        public override string Name => "Player Moved";

        public PlayerMovedEvent() : base()
        {

        }
    }
}
