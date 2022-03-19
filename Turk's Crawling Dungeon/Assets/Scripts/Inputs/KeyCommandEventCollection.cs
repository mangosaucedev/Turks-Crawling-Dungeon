using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public class KeyCommandEventCollection 
    {
        public bool isEnabled;

        private Dictionary<KeyCommand, KeyEventCollection> events =
            new Dictionary<KeyCommand, KeyEventCollection>();

        public void CheckKeyCommandState(KeyCommand command)
        {
            foreach (KeyState state in Enum.GetValues(typeof(KeyState)))
            {
                if (Keys.GetCommand(command, state))
                    SendKeyEvent(command, state);
            }
        }

        public void Subscribe(KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            keyEventCollection[state] += action;
        }

        public KeyEventCollection GetKeyEventCollection(KeyCommand command)
        {
            KeyEventCollection keyEventCollection;
            if (!events.TryGetValue(command, out keyEventCollection))
                keyEventCollection = RegisterEvents(command);
            return keyEventCollection;
        }

        public void Unsubscribe(KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            keyEventCollection[state] -= action;
        }

        public void SendKeyEvent(KeyCommand command, KeyState state)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            KeyEventContext context = new KeyEventContext(command, state);
            keyEventCollection[state]?.Invoke(context);
            EventManager.Send(KeyEvent.FromPool(context));
            return;
        }

        public KeyEventCollection RegisterEvents(KeyCommand command)
        {
            KeyEventCollection keyEventCollection = new KeyEventCollection();

            keyEventCollection[KeyState.Released] = delegate { };
            keyEventCollection[KeyState.ReleasedThisFrame] = delegate { };
            keyEventCollection[KeyState.Pressed] = delegate { };
            keyEventCollection[KeyState.PressedThisFrame] = delegate { };

            events[command] = keyEventCollection;
            return keyEventCollection;
        }
    }
}
