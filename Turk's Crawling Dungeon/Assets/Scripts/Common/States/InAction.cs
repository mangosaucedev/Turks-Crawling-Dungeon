using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;

namespace TCD
{
    public class InAction : GameplayState
    {
        public override string Name => "InAction";

        public override void Start()
        {
            base.Start();
            InputManager.SetInputGroupEnabled(InputGroup.Gameplay, false);
        }

        public override void End()
        {
            base.End();
            InputManager.SetInputGroupEnabled(InputGroup.Gameplay);
        }
    }
}
