using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Lootable : Part
    {
        [SerializeField] private bool generateLoot;

        private Inventory inventory;

        public override string Name => "Lootable";

        public bool GenerateLoot
        {
            get => generateLoot;
            set => generateLoot = value;
        }

        private Inventory Inventory
        {
            get
            {
                if (!inventory)
                    inventory = parent.Parts.Get<Inventory>();
                return inventory;
            }
        }


        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Loot", Loot));
        }

        private void Loot()
        {
            if (!Inventory)
            {
                ExceptionHandler.HandleMessage($"[Lootable] - {parent.name} does not have Inventory part!");
                return;
            }

            SelectionHandler.SetSelectedInventory( PlayerInfo.currentPlayer.Parts.Get<Inventory>());
            SelectionHandler.SetSelectedOtherInventory( Inventory);
            ViewManager.Open("Loot Inventory");
        }
    }
}
