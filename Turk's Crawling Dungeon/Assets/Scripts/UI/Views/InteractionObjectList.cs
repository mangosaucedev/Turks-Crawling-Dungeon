using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class InteractionObjectList : ViewController
    {
        [SerializeField] private Transform content;
        private Cell cell;
        private List<BaseObject> objects;
        private int buttonIndex;
        protected override string ViewName => gameObject.name;

        private void Start()
        {
            cell = SelectionHandler.SelectedCell;
            objects = cell.Objects;
            SetupButtons();   
        }

        private void SetupButtons()
        {
            buttonIndex = 0;
            foreach (BaseObject obj in objects)
            {
                if (obj.Parts.Get<Inspectable>())
                {
                    CreateButton(obj);
                    buttonIndex++;
                }
            }
            ViewButton closeButton = ViewButton.Create<ViewButton>("View Button", content);
            ButtonInputKey inputKey = ButtonInputKeys.GetInputKey(buttonIndex);
            if (inputKey != null)
                closeButton.key = inputKey.str;
            closeButton.onClick.AddListener(CloseView);
            closeButton.SetText("Close");
        }

        private void CreateButton(BaseObject obj)
        {
            ViewButton button = ViewButton.Create<ViewButton>("View Button", content);
            ButtonInputKey inputKey = ButtonInputKeys.GetInputKey(buttonIndex);
            if (inputKey != null)
                button.key = inputKey.str;
            button.onClick.AddListener(() => { OnObjectButtonClick(obj); });
            button.SetText(obj.GetDisplayName());
        }

        private void OnObjectButtonClick(BaseObject obj)
        {
            SelectionHandler.SetSelectedObject(obj);
            CloseView();
            ViewManager.Open("Interaction List");
        }
    }
}
