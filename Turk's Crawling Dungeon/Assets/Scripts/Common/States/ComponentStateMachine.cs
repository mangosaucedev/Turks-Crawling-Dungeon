using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public abstract class ComponentStateMachine : MonoBehaviour, IStateMachine
    {
#if UNITY_EDITOR
        [SerializeField] private string currentState;
#endif

        private IState state;
        private List<IState> states = new List<IState>();
        private Dictionary<string, IState> statesByName = new Dictionary<string, IState>();
        private HashSet<Transition> transitions = new HashSet<Transition>();
        private HashSet<TransitionFromAny> transitionsFromAny = new HashSet<TransitionFromAny>();

        public IState State
        {
            get => state;
            set => state = value;
        }

        public List<IState> States => states;

        public Dictionary<string, IState> StatesByName => statesByName;

        public HashSet<Transition> Transitions => transitions;

        public HashSet<TransitionFromAny> TransitionsFromAny => transitionsFromAny;

        public void Update()
        {
            UpdateAllTransitions();
            state?.Update();
#if UNITY_EDITOR
            currentState = (State?.Name ?? "None") + " | " + (State?.State?.Name ?? "None");
#endif
        }

        public void FixedUpdate()
        {
            state?.FixedUpdate();
        }

        public IState AddState(IState state, bool goTo = false)
        {
            States.Add(state);
            StatesByName[state.Name] = state;
            if (goTo)
                GoToState(state);
            return state;
        }

        public void GoToState(IState state)
        {
            this.state?.End();
            this.state = state;
            this.state?.Start();
            UpdateAllTransitions();
        }

        private void UpdateAllTransitions()
        {
            UpdateTransitions();
            UpdateTransitionsFromAny();
        }

        private void UpdateTransitions()
        {
            foreach (Transition transition in transitions)
            {
                if (State == transition.state && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                } 
            }
        }

        private void UpdateTransitionsFromAny()
        {
            foreach (TransitionFromAny transition in transitionsFromAny)
            {
                if (State != transition.goTo && transition.CanTransition())
                {
                    GoToState(transition.goTo);
                    return;
                }
            }
        }
    }
}
