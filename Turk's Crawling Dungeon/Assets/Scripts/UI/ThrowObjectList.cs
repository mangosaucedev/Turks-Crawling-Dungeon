using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Inputs.Actions;

namespace TCD.UI
{
    public class ThrowObjectList : ViewController
    {
        [SerializeField] private Transform content;
        private int buttonIndex;
        protected override string ViewName => gameObject.name;

        private void Start()
        {
            SetupButtons();
        }

        private void SetupButtons()
        {
            buttonIndex = 0;
            BaseObject player = PlayerInfo.currentPlayer;
            Inventory inventory = player.Parts.Get<Inventory>();
            foreach (BaseObject obj in inventory.items)
            {
                if (obj.Parts.Get<Throwable>())
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
            CloseView();
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            playerActionManager.TryStartAction(new Throw(obj), true);
        }
    }
}
