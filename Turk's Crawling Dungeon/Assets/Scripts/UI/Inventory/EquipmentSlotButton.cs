using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class EquipmentSlotButton : ViewButton
    {
        public EquipSlot equipSlot;
        
        [SerializeField] private bool overrideWithPlayerEquipment;
        [SerializeField] private Text equipmentHeader;
        [SerializeField] private Image equipmentImage;

        private BaseObject equipmentOwner;
        private Equipment equipment;
        private BaseObject equippedItem;

        protected override void OnEnable()
        {
            base.OnEnable();
            View.UpdateEvent += UpdateEquipment;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            View.UpdateEvent -= UpdateEquipment;
        }

        private void UpdateEquipment()
        {
            if (overrideWithPlayerEquipment)
                equipmentOwner = PlayerInfo.currentPlayer;
            else
                equipmentOwner = SelectionHandler.SelectedObject;
            if (equipmentOwner.Parts.TryGet(out equipment))
            {
                equippedItem = equipment.GetEquippedItem(equipSlot);
                if (equippedItem)
                    UpdateEquipmentImage();
                else
                    ChangeEquipmentImageToClear();
            }
        }

        private void UpdateEquipmentImage()
        {
            SpriteRenderer spriteRenderer = equippedItem.SpriteRenderer;
            equipmentImage.color = Color.white;
            equipmentImage.sprite = spriteRenderer.sprite;
        }

        private void ChangeEquipmentImageToClear()
        {
            equipmentImage.color = Color.clear;
            equipmentImage.sprite = null;
        }
    }
}
