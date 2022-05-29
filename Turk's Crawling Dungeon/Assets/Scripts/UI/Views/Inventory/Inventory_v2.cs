using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;

namespace TCD.UI
{
    public class Inventory_v2 : MonoBehaviour
    {
        [SerializeField] protected Inventory_v2ItemList itemList;
        [SerializeField] protected Inventory_v2ItemList otherItemList;

        protected virtual void Start()
        {
            BuildLists();
        }

        private void OnEnable()
        {
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            EventManager.StopListening<KeyEvent>(this);
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.state == KeyState.PressedThisFrame)
            {
                if (e.context.command == KeyCommand.Tab)
                    NextPage();
                if (e.context.command == KeyCommand.BackTab)
                    PreviousPage();
            } 
        }

        protected void BuildLists()
        {
            itemList?.BuildInventory(SelectionHandler.SelectedInventory);
            otherItemList?.BuildInventory(SelectionHandler.SelectedOtherInventory);
        }

        protected void NextPage()
        {
            UICursor cursor = ServiceLocator.Get<UICursor>();
            Vector2 position = Camera.main.ScreenToViewportPoint(cursor.position);
            if (position.x > 0.5f && otherItemList)
                otherItemList.NextPage();
            else
                itemList.NextPage();

        }

        protected void PreviousPage()
        {
            UICursor cursor = ServiceLocator.Get<UICursor>();
            Vector2 position = Camera.main.ScreenToViewportPoint(cursor.position);
            if (position.x > 0.5f && otherItemList)
                otherItemList.PreviousPage();
            else
                itemList.PreviousPage();
        }
    }
}
