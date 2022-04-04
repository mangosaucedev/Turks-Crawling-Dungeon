using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public abstract class InventoryView : ViewController
    {
        protected List<GameObject> inventoryElements = new List<GameObject>();
        protected Inventory currentInventory;
        protected Transform currentParent;

        protected override string ViewName => gameObject.name;

        protected override void OnEnable()
        {
            base.OnEnable();
            View.UpdateEvent += UpdateInventories;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            View.UpdateEvent -= UpdateInventories;
        }

        protected abstract void UpdateInventories();
    }
}
