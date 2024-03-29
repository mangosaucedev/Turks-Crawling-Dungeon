using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Inspectable : Part
    {
        public override string Name => "Inspectable";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Inspect", OnInspect));
        }

        private void OnInspect()
        {
            BeforeInspectEvent e = LocalEvent.Get<BeforeInspectEvent>();
            e.obj = parent;
            if (FireEvent(parent, e))
            {
                SelectionHandler.SetSelectedObject(parent);
                ViewManager.Open("Inspect View");
            }
        }
    }
}
