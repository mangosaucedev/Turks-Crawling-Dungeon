using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Equipment : Part
    {
        public List<BaseObject> equippedItems = new List<BaseObject>();
        private Dictionary<EquipSlot, BaseObject> equippedItemsBySlot =
            new Dictionary<EquipSlot, BaseObject>();
        private Inventory inventory;

        public override string Name => "Equipment";

        public Inventory Inventory
        {
            get
            {
                if (!inventory)
                    inventory = parent.parts.Get<Inventory>();
                return inventory;
            }
        }

        public bool TryEquip(BaseObject equipment)
        {
            if (!equipment.parts.TryGet(out Item item) || !equipment.parts.TryGet(out Equippable equippable) ||
                (item.inventory != Inventory && !Inventory.TryAddItem(equipment)))
                return false;
            return EquipToSlots(equipment);
        }

        public bool EquipToSlots(BaseObject equipment)
        {
            Equippable equippable = equipment.parts.Get<Equippable>();
            if (!TryGetAvailableSlots(equippable, out List<EquipSlot> availableSlots))
                return false;
            if (!equippable.RequiresSecondSlot)
            {
                if (equippable.equipSlots.Length == 1)
                {
                    EquipSlot slot = equippable.equipSlots[0];
                    if (!EquipToSlot(equipment, slot))
                        return false;
                }
                else
                {
                    EquipSlot primarySlot = equippable.equipSlots[0];
                    EquipSlot secondarySlot = equippable.equipSlots[1];
                    bool primaryOccupied = GetEquippedItem(primarySlot);
                    bool canEquipToPrimary = false;
                    bool canEquipToSecondary = false;
                    if (!primaryOccupied && (canEquipToPrimary = EquipToSlot(equipment, primarySlot)))
                        return true;
                    if (primaryOccupied && (canEquipToSecondary = EquipToSlot(equipment, secondarySlot)))
                        return true;
                    if (!canEquipToPrimary && !canEquipToSecondary)
                        return false;
                }
            }
            else
                return EquipToAllSlots(equipment, availableSlots);
            return true;
        }

        private bool TryGetAvailableSlots(Equippable equippable, out List<EquipSlot> availableSlots)
        {
            availableSlots = new List<EquipSlot>();
            foreach (EquipSlot slot in equippable.equipSlots)
            {
                bool canEquipToSlot = CanEquipToSlot(slot);
                if (!canEquipToSlot && equippable.RequiresSecondSlot)
                    return false;
                else if (canEquipToSlot)
                    availableSlots.Add(slot);
            }
            return true;
        }

        private bool CanEquipToSlot(EquipSlot slot)
        {
            BaseObject currentlyEquipped = GetEquippedItem(slot);
            if (currentlyEquipped && !CanUnequip(currentlyEquipped))
                return false;
            return true;
        }

        public bool EquipToAllSlots(BaseObject equipment, List<EquipSlot> slots)
        {
            foreach (EquipSlot slot in slots)
            {
                if (!EquipToSlot(equipment, slot))
                {
                    TryUnequip(equipment);
                    return false;
                }
            }    
            return true;
        }

        public bool EquipToSlot(BaseObject equipment, EquipSlot slot)
        {
            equippedItems.Add(equipment);
            equippedItemsBySlot[slot] = equipment;
            Equippable equippable = equipment.parts.Get<Equippable>();
            equippable.equipment = this;
            return true;
        }

        public bool TryUnequip(BaseObject item)
        {
            if (!CanUnequip(item))
                return false;
            equippedItems.Remove(item);
            Equippable equippable = item.parts.Get<Equippable>();
            foreach (EquipSlot slot in equippable.equipSlots)
            {
                if (GetEquippedItem(slot) == item)
                    equippedItemsBySlot.Remove(slot);
            }
            equippable.equipment = null;
            return true;
        }

        private bool CanUnequip(BaseObject item)
        {
            if (!equippedItems.Contains(item))
                return false;
            return true;
        }

        public BaseObject GetEquippedItem(EquipSlot slot)
        {
            if (!equippedItemsBySlot.TryGetValue(slot, out BaseObject equippedItem))
            {
                equippedItem = null;
                equippedItemsBySlot[slot] = null;
            }
            return equippedItem;
        }

        public override bool HandleEvent<T>(T e)
        {
            bool successful = true;
            foreach (BaseObject item in equippedItems)
            {
                if (!FireEvent(item, e))
                    successful = false;
            }
            return base.HandleEvent(e) && successful;
        }
    }
}
