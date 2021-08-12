using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs.Actions;

namespace TCD
{
    public class PlayerActionStartedEvent : Event
    {
        public PlayerAction action;

        public override string Name => "Player Action Started";

        public PlayerActionStartedEvent(PlayerAction action) : base()
        {
            this.action = action;
        }
    }
}
