using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD
{
    public class BeforeViewClosedEvent : Event
    {
        public ActiveView view;

        public override string Name => "Before View Closed";

        public BeforeViewClosedEvent(ActiveView view) : base()
        {
            this.view = view;
        }
    }
}