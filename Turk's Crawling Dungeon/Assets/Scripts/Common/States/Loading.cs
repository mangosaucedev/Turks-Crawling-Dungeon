using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;
using TCD.Inputs;

namespace TCD
{
    public class Loading : GameplayState
    {
        public override string Name => "Loading";

        private InputManager InputManager => ServiceLocator.Get<InputManager>();

        public override void Start()
        {
            base.Start();
            if (Assets.FindAll<GameObject>("Loading View").Count > 0)
                ViewManager.Open("Loading View");
        }

        public override void End()
        {
            base.End();
            ViewManager.Close("Loading View");
        }
    }
}
