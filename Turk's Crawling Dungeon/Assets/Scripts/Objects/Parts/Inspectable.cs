using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
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
            SelectionHandler.SetSelectedObject(parent);
            ViewManager.Open("Inspect View");
        }
    }
}
