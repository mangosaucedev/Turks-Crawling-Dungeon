using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD
{
    public class ViewClosedEvent : Event
    {
        public ActiveView view;

        public override string Name => "View Closed";

        public ViewClosedEvent(ActiveView view) : base()
        {
            this.view = view;
        }
    }
}
