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

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            cell = SelectionHandler.SelectedCell;
            objects = cell.objects;
            SetupButtons();   
        }

        private void SetupButtons()
        {
            foreach (BaseObject obj in objects)
            {
                if (obj.parts.Get<Inspectable>())
                    CreateButton(obj);
            }
            ViewButton closeButton = ViewButton.Create<ViewButton>("View Button", content);
            closeButton.onClick.AddListener(CloseView);
            closeButton.SetText("Close");
        }

        private void CreateButton(BaseObject obj)
        {
            ViewButton button = ViewButton.Create<ViewButton>("View Button", content);
            button.onClick.AddListener(() => { OnObjectButtonClick(obj); });
            button.SetText(obj.display.GetDisplayName());
        }

        private void OnObjectButtonClick(BaseObject obj)
        {
            SelectionHandler.SetSelectedObject(obj);
            CloseView();
            ViewManager.Open("Interaction List");
        }
    }
}
