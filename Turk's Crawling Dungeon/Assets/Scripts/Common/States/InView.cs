using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;

namespace TCD
{
    public class InView : GameplayState
    {
        public override string Name => "InView";

        public override void Start()
        {
            base.Start();
            InputManager.SetInputGroupEnabled(InputGroup.UI);
        }

        public override void End()
        {
            base.End();
            InputManager.SetInputGroupEnabled(InputGroup.UI, false);
        }
    }
}
