using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class Inventory_v2ItemList : MonoBehaviour
    {
        [SerializeField] private View view;
        [SerializeField] private GameObject itemButtonPrefab;
        [SerializeField] private GameObject weightLabelPrefab;
        [SerializeField] private Transform buttonParent;
        [SerializeField] private Transform weightParent;

        private Inventory inventory;
        private Inventory_v2ItemPages pages;
        
        private void Awake()
        {
            if (!view)
                view = GetComponentInParent<View>();
        }

        private void OnEnable()
        {
            view.SetActiveEvent += () => { BuildInventory(inventory); };
        }

        private void OnDisable()
        {
            view.SetActiveEvent -= () => { BuildInventory(inventory); };
        }

        public void BuildInventory(Inventory inventory)
        {
            this.inventory = inventory;
            pages?.Destroy();
            if (inventory)
            {
                pages = new Inventory_v2ItemPages(inventory);
                pages.SetPrefabs(itemButtonPrefab, weightLabelPrefab);
                pages.SetParents(buttonParent, weightParent);
                pages.SortPages();
            }
        }

        public void NextPage()
        { 
            if (pages.NextPage())
                return;
        }

        public void PreviousPage()
        {
            if (pages.PreviousPage())
                return;
        }
    }
}
