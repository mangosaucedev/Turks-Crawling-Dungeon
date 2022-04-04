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
            InputManager.SetInputGroupEnabled(InputGroup.UI);
            base.Start();
        }

        public override void End()
        {
            InputManager.SetInputGroupEnabled(InputGroup.UI, false);
            base.End();
        }
    }
}
