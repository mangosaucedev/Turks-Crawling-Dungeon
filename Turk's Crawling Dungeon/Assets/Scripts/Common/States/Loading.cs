using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;
using TCD.Inputs;

namespace TCD
{
    public class Loading : GameplayState
    {
        private IState inView;

        public override string Name => "Loading";

        public Loading()
        {
            inView = AddState(GameplayStateFactory.InView());
            Transitions.Add(new Transition(inView, () => { return !GoToInView(); }, null));
            TransitionsFromAny.Add(new TransitionFromAny(GoToInView, inView));
        }

        public override void Start()
        {
            InputManager.SetInputGroupEnabled(InputGroup.None, false);
            if (Assets.FindAll<GameObject>("Loading View").Count > 0)
                ViewManager.Open("Loading View");
            base.Start();
        }

        public override void End()
        {
            InputManager.SetInputGroupEnabled(InputGroup.None);
            ViewManager.Close("Loading View");
            base.End();
        }
    }
}
