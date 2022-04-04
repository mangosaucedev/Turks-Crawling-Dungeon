using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;

namespace TCD
{
    public class InCinematic : GameplayState
    {
        private IState inView;

        public override string Name => "InCinematic";

        public InCinematic() : base()
        {
            inView = AddState(GameplayStateFactory.InView());
            Transitions.Add(new Transition(inView, () => { return !GoToInView(); }, null));
            TransitionsFromAny.Add(new TransitionFromAny(GoToInView, inView));
        }

        public override void Start()
        {
            base.Start();
            CinematicManager cinematicManager = ServiceLocator.Get<CinematicManager>();
            cinematicManager.StartCinematic();
        }
    }
}
