using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class EventOneShotActionCollection
    {
        private Dictionary<object, List<Action>> oneShotActions = 
            new Dictionary<object, List<Action>>();
        private List<object> listeners = new List<object>();

        public void AddOneShotAction(object listener, Action action)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
            List<Action> actions = GetActionsForListener(listener);
            actions.Add(action);
        }

        private List<Action> GetActionsForListener(object listener)
        {
            if (!oneShotActions.TryGetValue(listener, out var actions))
            {
                actions = new List<Action>();
                oneShotActions[listener] = actions;
            }
            return actions;
        }

        public void PerformOneShotActions()
        {
            TrimNullListeners();
            foreach (object listener in listeners)
            {
                List<Action> actions = GetActionsForListener(listener);
                for (int i = actions.Count - 1; i >= 0; i--)
                {
                    Action action = actions[i];
                    action?.Invoke();
                    actions.RemoveAt(i);
                }
            }
            oneShotActions.Clear();
            listeners.Clear();
        }

        private void TrimNullListeners()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                object listener = listeners[i];
                if (listener == null)
                    listeners.RemoveAt(i);
            }
        }
    }
}
