using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Item : Part
    {
        public Inventory inventory;

        public BaseObject Owner => inventory?.parent;

        public bool IsInInventory => inventory;

        public bool IsInPlayerInventory => Owner != null && Owner == PlayerInfo.currentPlayer;

        public override string Name => "Item";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            if (!IsInPlayerInventory)
                e.interactions.Add(new Interaction("Get", OnPlayerGet));
            else
                e.interactions.Add(new Interaction("Drop", OnPlayerDrop));
        }

        public void PutInPlayerInventory() => OnPlayerGet();

        protected void OnPlayerGet()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Inventory playerInventory = player.parts.Get<Inventory>();
            if (playerInventory.TryAddItem(parent))
            {
                PickedUpEvent pickedUpEvent = LocalEvent.Get<PickedUpEvent>();
                parent.HandleEvent(pickedUpEvent);
            }
        }

        protected void OnPlayerDrop()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Inventory playerInventory = player.parts.Get<Inventory>();
            if (playerInventory.TryRemoveItem(parent))
            {
                DroppedEvent droppedEvent = LocalEvent.Get<DroppedEvent>();
                parent.HandleEvent(droppedEvent);
            }
        }
    }
}
