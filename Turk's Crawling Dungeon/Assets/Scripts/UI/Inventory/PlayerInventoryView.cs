using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class PlayerInventoryView : InventoryView
    {
        [SerializeField] private Transform carriedItemsParent;

        private int buttonIndex;

        protected override void Awake()
        {
            base.Awake();
            BaseObject player = PlayerInfo.currentPlayer;
            currentInventory = player.parts.Get<Inventory>();
        }

        protected override void UpdateInventories()
        {
            DestroyInventoryElements();
            UpdateCarriedItems();
            UpdateEquippedItems();
        }

        private void DestroyInventoryElements()
        {
            foreach (GameObject inventoryElement in inventoryElements)
                Destroy(inventoryElement);
        }

        private void UpdateCarriedItems()
        {
            buttonIndex = 0;
            foreach (BaseObject item in currentInventory.items)
            {
                CreateButton(item);
                buttonIndex++;
            }
        }

        private void UpdateEquippedItems()
        {

        }

        private void CreateButton(BaseObject item)
        {
            ViewButton button = ViewButton.Create<ViewButton>("Item Button", carriedItemsParent);
            ButtonInputKey inputKey = ButtonInputKeys.GetInputKey(buttonIndex);
            if (inputKey != null)
                button.key = inputKey.str;
            button.onClick.AddListener(() => { OnItemButtonClick(item); });
            button.SetText(item.display.GetDisplayName());
            inventoryElements.Add(button.gameObject);
        }

        private void OnItemButtonClick(BaseObject item)
        {
            SelectionHandler.SetSelectedObject(item);
            ViewManager.Open("Interaction List");
        }
    }
}
