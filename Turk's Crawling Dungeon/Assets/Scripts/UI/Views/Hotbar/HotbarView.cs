using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.Inputs.Hotbar;
using TCD.Objects;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class HotbarView : ViewController
    {
        private List<HotbarButton> hotbarButtons = new List<HotbarButton>();
        private int hotbarIndex;

        protected override string ViewName => gameObject.name;

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<PlayerCreatedEvent>(this, OnPlayerCreated);
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<PlayerCreatedEvent>(this);
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnPlayerCreated(PlayerCreatedEvent e)
        {
            UpdateHotbarButtons();
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            UpdateHotbarButtons();
        }

        private void UpdateHotbarButtons()
        {
            ClearHotbarButtons();
            BuildHotbarButtons();
        }

        private void ClearHotbarButtons()
        {
            for (int i = hotbarButtons.Count - 1; i >= 0; i--)
            {
                ViewButton hotbarButton = hotbarButtons[i];
                if (hotbarButton)
                    Destroy(hotbarButton.gameObject);
            }
        }

        private void BuildHotbarButtons()
        {
            if (!PlayerInfo.currentPlayer.Parts.TryGetList(out List<Talent> talents))
                return;
            hotbarIndex = 0;
            foreach (Talent talent in talents)
            {
                BuildHotbarButton(talent);
                hotbarIndex++;
            }
        }

        private void BuildHotbarButton(Talent talent)
        {
            HotbarButton hotbarButton = ViewButton.Create<HotbarButton>("Hotbar Button", transform);
            hotbarButton.talent = talent;
            if (hotbarIndex < Hotbar.hotbarCommands.Length)
                hotbarButton.keyCommand = Hotbar.hotbarCommands[hotbarIndex];
            hotbarButtons.Add(hotbarButton);
        }
    }
}
