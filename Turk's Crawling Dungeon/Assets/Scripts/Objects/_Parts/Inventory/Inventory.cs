using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Inventory : Part
    {
        public List<BaseObject> items = new List<BaseObject>();

        [SerializeField] private float baseMaxWeight;
        [SerializeField] private string spawnEncounterTable;

        private Encounter encounter;

        public float BaseMaxWeight
        {
            get => baseMaxWeight;
            set => baseMaxWeight = value;
        }

        public string SpawnEncounterTable
        {
            get => spawnEncounterTable;
            set => spawnEncounterTable = value;
        }

        public Encounter Encounter 
        {
            get
            {
                if (encounter == null)
                    encounter = EncounterFactory.BuildFromBlueprint(SpawnEncounterTable);
                return encounter;
            }
        }

        public override string Name => "Inventory";

        protected override void Start()
        {
            base.Start();
            if (!string.IsNullOrEmpty(SpawnEncounterTable))
                AddEncounterTable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                BaseObject item = items[i];
                RemoveItem(item);
            }
        }

        private void AddEncounterTable()
        {
            var objects = Encounter.BuildForcedObjects(Position);
            foreach (var obj in objects)
                TryAddItem(obj);
        }

        public float GetWeight()
        {
            float weight = 0f;
            foreach (BaseObject item in items)
                weight += GetItemWeight(item);
            return weight;
        }

        private float GetItemWeight(BaseObject item)
        {
            if (item.Parts.TryGet(out PhysicsSim physics))
                return physics.GetWeight();
            return 0f;
        }

        public float GetMaxWeight()
        {
            GetMaxWeightEvent e = LocalEvent.Get<GetMaxWeightEvent>();
            e.maxWeight = BaseMaxWeight;
            FireEvent(parent, e);
            return e.maxWeight;
        }

        public bool TryAddItem(BaseObject item)
        {
            if (!CanSupportItemWeight(item))
                return false;
            AddItem(item);
            return true;
        }

        private bool CanSupportItemWeight(BaseObject item)
        {
            float currentWeight = GetWeight();
            float weight = GetItemWeight(item);
            float maxWeight = GetMaxWeight();
            return (currentWeight + weight) <= maxWeight;
        }

        public void AddItem(BaseObject item)
        {
            items.Add(item);
            item.cell.SetPosition(0, 0);
            item.deactivator.AddDeactivatingCondition(this);
            Item i = item.Parts.Get<Item>();
            i.inventory = this;
        }

        public bool TryRemoveItem(BaseObject item)
        {
            if (CannotUnequipEquippedItem(item))
                return false;
            RemoveItem(item);
            return true;
        }   

        public bool CannotUnequipEquippedItem(BaseObject item)
        {
            if (parent.Parts.TryGet(out Equipment equipment) &&
                equipment.equippedItems.Contains(item) &&
                !equipment.TryUnequip(item))
                return true;
            return false;
        }
        
        public void RemoveItem(BaseObject item)
        {
            items.Remove(item);
            item.cell.SetPosition(Position);
            item.deactivator.RemoveDeactivatingCondition(this);
            if (item.Parts.TryGet(out Item i))
                i.inventory = null;
            else if (item)
                DebugLogger.LogError($"{item.name} cannot be removed from inventory; no Item Part attached!");
        }
    }
}
