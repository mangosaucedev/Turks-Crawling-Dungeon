using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Lootable : Part
    {
        private Inventory inventory;

        private Inventory Inventory
        {
            get
            {
                if (!inventory)
                    inventory = parent.Parts.Get<Inventory>();
                return inventory;
            }
        }

        public override string Name => "Lootable";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Loot", Loot));
        }

        private void Loot()
        {

        }
    }
}
