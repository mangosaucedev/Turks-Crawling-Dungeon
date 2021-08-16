using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class InteractionList : ViewController
    {
        [SerializeField] private Text title;
        [SerializeField] private Transform content;
        private BaseObject obj;
        private List<Interaction> interactions;
        private int buttonIndex;

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            obj = SelectionHandler.SelectedObject;
            title.text = $"/ Interact with {obj.display.GetDisplayName()}... /";
            SetupButtons();
        }

        private void SetupButtons()
        {
            GetInteractions();
            buttonIndex = 0;
            foreach (Interaction interaction in interactions)
            {
                CreateButton(interaction);
                buttonIndex++;
            }
            ViewButton closeButton = ViewButton.Create<ViewButton>("View Button", content);
            ButtonInputKey inputKey = ButtonInputKeys.GetInputKey(buttonIndex);
            if (inputKey != null)
                closeButton.key = inputKey.str;
            closeButton.onClick.AddListener(CloseView);
            closeButton.SetText("Close");
        }

        private void GetInteractions()
        {
            GetInteractionsEvent e = LocalEvent.Get<GetInteractionsEvent>();
            obj.HandleEvent(e);
            interactions = e.interactions;
        }

        private void CreateButton(Interaction interaction)
        {
            ViewButton button = ViewButton.Create<ViewButton>("View Button", content);
            ButtonInputKey inputKey = ButtonInputKeys.GetInputKey(buttonIndex);
            if (inputKey != null)
                button.key = inputKey.str;
            button.onClick.AddListener(() => { OnInteractionButtonClick(interaction); });
            button.SetText(interaction.name);
        }

        private void OnInteractionButtonClick(Interaction interaction)
        {
            interaction.onInteract?.Invoke();
            CloseView();
        }
    }
}
