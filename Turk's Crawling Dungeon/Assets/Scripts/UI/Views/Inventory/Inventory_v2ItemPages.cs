using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class Inventory_v2ItemPages
    {
        private const int ITEMS_TO_PAGE = 16;

        private Inventory inventory;
        private GameObject buttonPrefab;
        private GameObject weightPrefab;
        private GameObject xPrefab;
        private Transform buttonParent;
        private Transform weightParent;
        private Transform xParent;
        private List<List<Inventory_v2Item>> pages = new List<List<Inventory_v2Item>>();
        private int pageCount;
        private uint page;

        public uint Page => page;

        public int Count => pages.Count;

        public Inventory_v2ItemPages(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void SetPrefabs(GameObject buttonPrefab = null, GameObject weightPrefab = null, GameObject xPrefab = null)
        {
            this.buttonPrefab = buttonPrefab;
            this.weightPrefab = weightPrefab;
            this.xPrefab = xPrefab;
        }

        public void SetParents(Transform buttonParent = null, Transform weightParent = null, Transform xParent = null)
        {
            this.buttonParent = buttonParent;
            this.weightParent = weightParent;
            this.xParent = xParent;
        }

        public void SortPages()
        {
            int count = CountPages();
            for (int i = 0; i < count; i++)
                SortPage(i);
            GoToPage(0);
        }

        private int CountPages()
        {
            int pages = 1;
            int items = 0;
            foreach (BaseObject item in inventory.items)
            {
                items++;
                if (items == ITEMS_TO_PAGE)
                {
                    pages++;
                    items = 1;
                }
            }
            return pageCount = pages;
        }

        private void SortPage(int index)
        {
            int startIndex = index * ITEMS_TO_PAGE;
            int endIndex = startIndex + ITEMS_TO_PAGE;
            List<Inventory_v2Item> page = new List<Inventory_v2Item>();
            
            for (int i = startIndex; i < endIndex; i++)
            {
                if (i >= inventory.items.Count)
                    break;
                BaseObject itemObject = inventory.items[i];
                GameObject buttonObject = BuildButtonObject(itemObject);
                GameObject weightObject = BuildWeightObject(itemObject);
                GameObject xObject = null;
                Inventory_v2Item item = new Inventory_v2Item(itemObject);
                item.AssignObjects(buttonObject, weightObject, xObject);
                item.Disable();
                page.Add(item);
            }

            pages.Add(page);
        }

        private GameObject BuildButtonObject(BaseObject itemObject)
        {
            GameObject gameObject = Object.Instantiate(buttonPrefab, buttonParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.SetText(itemObject.GetDisplayName());
            viewButton.onClick.AddListener(() => { ButtonOnClick(itemObject); });
            return gameObject;
        }

        private void ButtonOnClick(BaseObject itemObject)
        {
            SelectionHandler.SetSelectedObject(itemObject);
            ViewManager.Open("Interaction List");
        }

        private GameObject BuildWeightObject(BaseObject itemObject)
        {
            GameObject gameObject = Object.Instantiate(weightPrefab, weightParent);
            Text text = gameObject.GetComponentInChildren<Text>();
            text.text = itemObject.Parts.Get<PhysicsSim>()?.GetWeight().ToString() ?? "0";
            return gameObject;
        }

        public List<Inventory_v2Item> GetCurrentPageItems() => pages[(int) page];

        public bool NextPage()
        {
            if (Page >= pageCount - 1)
                return false;
            GoToPage(Page + 1);
            return true;
        }

        public bool PreviousPage()
        {
            if (Page <= 0)
                return false;
            GoToPage(Page - 1);
            return true;
        }

        private void GoToPage(uint index)
        {
            List<Inventory_v2Item> page;

            if (pages.Count >= index)
            {
                page = pages[(int)Page];
                for (int i = page.Count - 1; i >= 0; i--)
                    page[i].Disable();
            }

            this.page = index;

            page = pages[(int) index];
            for (int i = page.Count - 1; i >= 0; i--)
                page[i].Enable();
        }

        public void Destroy()
        {
            for (int i = pageCount - 1; i >= 0; i--)
            {
                var page = pages[i];
                for (int j = page.Count - 1; j >= 0; j--)
                    page[j].Destroy();
            }
        }
    }
}
