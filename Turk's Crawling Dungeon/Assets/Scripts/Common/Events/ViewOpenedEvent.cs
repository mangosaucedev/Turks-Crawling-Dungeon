using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD
{
    public class ViewOpenedEvent : Event
    {
        public ActiveView view;

        public override string Name => "View Opened";

        public ViewOpenedEvent(ActiveView view) : base()
        {
            this.view = view;
        }
    }
}
