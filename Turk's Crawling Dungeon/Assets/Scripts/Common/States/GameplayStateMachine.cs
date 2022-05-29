using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;
using TCD.UI;

namespace TCD
{
    public class GameplayStateMachine : ComponentStateMachine
    {
        private IState loading;
        private IState inView;
        private IState inCinematic;
        private IState gameplay;

        private void Awake()
        {
            loading = AddState(GameplayStateFactory.Loading());
            inView = AddState(GameplayStateFactory.InView());
            inCinematic = AddState(GameplayStateFactory.InCinematic());
            gameplay = AddState(GameplayStateFactory.Gameplay());
            Transitions.Add(new Transition(loading, () => { return !GoToLoading(); }, gameplay));
            Transitions.Add(new Transition(inView, () => { return !GoToInView(); }, gameplay));
            Transitions.Add(new Transition(inCinematic, () => { return !GoToInCinematic(); }, gameplay));
            TransitionsFromAny.Add(new TransitionFromAny(GoToLoading, loading));
            TransitionsFromAny.Add(new TransitionFromAny(GoToInView, inView));
            TransitionsFromAny.Add(new TransitionFromAny(GoToInCinematic, inCinematic));
        }

        private bool GoToLoading()
        {
            LoadingManager loadingManager = ServiceLocator.Get<LoadingManager>();
            return loadingManager.CurrentLoadingOperation != null;
        }

        private bool GoToInView()
        {
            if (State == loading || State == inCinematic)
                return false;
            return ViewManager.ActiveViewLocksInput();
        }

        private bool GoToInCinematic()
        {
            if (State == loading)
                return false;
            CinematicManager cinematicManager = ServiceLocator.Get<CinematicManager>();
            return cinematicManager.CurrentCinematic != null;
        }
    }
}
