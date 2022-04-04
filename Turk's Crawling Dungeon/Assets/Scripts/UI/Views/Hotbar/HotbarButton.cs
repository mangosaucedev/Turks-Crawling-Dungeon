using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.Inputs.Actions;
using TCD.Objects.Parts.Talents;
using TCD.Texts;

namespace TCD.UI
{
    public class HotbarButton : ImageButton
    {
        public Talent talent;
        public KeyCommand keyCommand;

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(UseTalentAction);
        }

        protected override void Start()
        {
            base.Start();
            icon.sprite = talent.Icon;
            keyText.text = keyCommand == KeyCommand.None ? "" : keyCommand.ToString().Replace("Hotbar", "");
            float activeCooldown = ((float) talent.activeCooldown / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);
            SetText(activeCooldown <= 0 ? "" : activeCooldown.ToString());
        }

        protected override void Update()
        {
            base.Update();
            if (Keys.GetCommand(keyCommand, KeyState.PressedThisFrame))
                onClick?.Invoke();
        }

        private void UseTalentAction()
        {
            if (!talent.CanUseTalent())
            {
                FloatingTextHandler.Draw(PlayerInfo.currentPlayer.transform.position, "Can't use that talent.");
                return;
            }
            PlayerActionManager actionManager = ServiceLocator.Get<PlayerActionManager>();
            actionManager.TryStartAction(new UseTalent(talent));
        }
    }
}
