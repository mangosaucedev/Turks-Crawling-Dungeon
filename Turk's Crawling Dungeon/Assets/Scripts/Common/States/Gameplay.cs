using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.Inputs.Actions;
using TCD.UI;

namespace TCD
{
    public class Gameplay : GameplayState
    {
        private IState inView;
        private IState inAction;
        private bool viewsEnabled;

        public override string Name => "Gameplay";

        private PlayerActionManager PlayerActionManager => ServiceLocator.Get<PlayerActionManager>();

        public Gameplay() : base()
        {
            inView = AddState(GameplayStateFactory.InView());
            inAction = AddState(GameplayStateFactory.InAction());
            Transitions.Add(new Transition(inAction, () => { return !GoToInAction(); }, null));
            TransitionsFromAny.Add(new TransitionFromAny(GoToInAction, inAction));
        }

        public override void Start()
        {
            InputManager.SetInputGroupEnabled(InputGroup.Gameplay);
            viewsEnabled = false;
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            if (!viewsEnabled && GameStartup.isFinished)
            {
                viewsEnabled = true;
                ViewManager.Open("Side Bar", false, false);
                ViewManager.Open("Score Counter", false, false);
                ViewManager.Open("Level Counter", false, false);
                ViewManager.Open("Hotbar", false, false);
                ViewManager.Open("Log", false, false);
            }
        }

        public override void End()
        {
            PlayerActionManager.CancelCurrentAction();
            if (viewsEnabled)
            {
                ViewManager.Close("Side Bar");
                ViewManager.Close("Score Counter");
                ViewManager.Close("Level Counter");
                ViewManager.Close("Hotbar");
                ViewManager.Close("Log");
            }
            InputManager.SetInputGroupEnabled(InputGroup.Gameplay, false);
            base.End();
        }        
        
        private bool GoToInAction()
        {
            if (State == inView)
                return false;
            return PlayerActionManager.currentActionRoutine != null;
        }
    }
}
