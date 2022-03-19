using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class StateMachine
    {
        private List<IState> states = new List<IState>();
        private List<Transition> transitions = new List<Transition>();
        private List<TransitionFromAny> transitionsFromAny = new List<TransitionFromAny>();
        private IState currentState;
        private string logName;
        private bool logStateChanges;

        public string State => currentState?.Name;

        public void Update()
        {
            currentState?.Update();
        }

        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void SlowUpdate()
        {
            currentState?.SlowUpdate();
        }

        public bool AddState(IState state, bool startInState = false)
        {
            if (states.Contains(state))
                return false;
            states.Add(state);
            if (startInState)
                GoToState(state);
            return true;
        }

        private bool GoToState(IState state)
        {
            if (logStateChanges)
            {
                string previousState = currentState?.Name ?? "NONE";
                string nextState = state.Name;
                DebugLogger.Log(logName + " changing states! " + previousState + " ---> " + nextState);
            }
            currentState?.End();
            currentState = state;
            currentState?.Start();
            return true;
        }

        public bool GoToState(string stateName)
        {
            IState state = Find(stateName);
            return GoToState(state);
        }

        private IState Find(string stateName)
        {
            foreach (IState state in states)
            {
                if (state.Name == stateName)
                    return state;
            }
            return null;
        }

        public bool RemoveState(string stateName)
        {
            return true;
        }

        public void EnableLogging(string stateMachineName)
        {
            logName = stateMachineName;
            logStateChanges = true;
        }

        public void DisableLogging()
        {
            logStateChanges = false;
        }
    }
}
