using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IStateMachine
    {
        IState State { get; set; }

        List<IState> States { get; }

        Dictionary<string, IState> StatesByName { get; }

        HashSet<Transition> Transitions { get; }

        HashSet<TransitionFromAny> TransitionsFromAny { get; }

        void Update();

        void FixedUpdate();

        IState AddState(IState state, bool goTo);

        void GoToState(IState state);
    }
}
