using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public class KeyEventCollection
    {
        private Dictionary<KeyState, KeyEventDelegate> actions = 
            new Dictionary<KeyState, KeyEventDelegate>();
    
        public KeyEventDelegate this[KeyState state]
        {
            get => actions[state];
            set => actions[state] = value;
        }

        public KeyEventCollection()
        {
            foreach (KeyState state in Enum.GetValues(typeof(KeyState)))
                actions[state] = e => { };
        }

        public void Add(KeyState state, KeyEventDelegate action)
        {
            actions[state] = action;
        }

        public void Remove(KeyState state)
        {
            actions.Remove(state);
        }
    }
}
