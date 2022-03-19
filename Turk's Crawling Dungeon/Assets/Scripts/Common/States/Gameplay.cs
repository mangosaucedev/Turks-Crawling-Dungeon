using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.UI;

namespace TCD
{
    public class Gameplay : GameplayState
    {
        private bool isEnabled;

        public override string Name => "Gameplay";

        private InputManager InputManager => ServiceLocator.Get<InputManager>();

        public override void Start()
        {
            base.Start();
            isEnabled = false;
        }

        public override void Update()
        {
            base.Update();
            if (!isEnabled)
            {
                isEnabled = true;
                InputManager.SetInputGroupEnabled(InputGroup.Gameplay);
            }
        }

        public override void End()
        {
            base.End();
            ViewManager.CloseAllInactive();
            InputManager.SetInputGroupEnabled(InputGroup.Gameplay, false);
        }
    }
}
