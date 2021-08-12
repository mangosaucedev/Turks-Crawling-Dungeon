using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public class KeyEventManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [Serializable]
        public struct Keybind
        {
            public KeyCommand command;
            public KeyCode key;
            public KeyCode primaryModifier;
            public KeyCode secondaryModifier;

            public Keybind(KeyCommand command, KeyCode key, 
                KeyCode primaryModifier, KeyCode secondaryModifier)
            {
                this.command = command;
                this.key = key;
                this.primaryModifier = primaryModifier;
                this.secondaryModifier = secondaryModifier;
            }
        }
#endif

        private static Dictionary<KeyCommand, KeyEventCollection> events = 
            new Dictionary<KeyCommand, KeyEventCollection>();

#if UNITY_EDITOR
        public static List<Keybind> keybinds = new List<Keybind>();
        
        [SerializeField] private List<Keybind> loadedKeybindings;
#endif

        private void Update()
        {
            foreach (KeyCommand command in Enum.GetValues(typeof(KeyCommand)))
                CheckKeyCommandState(command);

#if UNITY_EDITOR
            loadedKeybindings = keybinds;
#endif
        }

        private static KeyEventCollection GetKeyEventCollection(KeyCommand command)
        {
            KeyEventCollection keyEventCollection;
            if (!events.TryGetValue(command, out keyEventCollection))
                keyEventCollection = RegisterEvents(command);
            return keyEventCollection;
        }

        private static void CheckKeyCommandState(KeyCommand command)
        {
            foreach (KeyState state in Enum.GetValues(typeof(KeyState)))
            {
                if (Keys.GetCommand(command, state))
                    SendKeyEvent(command, state);
            }
        }

        private static void SendKeyEvent(KeyCommand command, KeyState state)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            KeyEventContext context = new KeyEventContext(command, state);
            keyEventCollection[state]?.Invoke(context);
            EventManager.Send(KeyEvent.FromPool(context));
            return;
        }

        private static KeyEventCollection RegisterEvents(KeyCommand command)
        {
            KeyEventCollection keyEventCollection = new KeyEventCollection();

            keyEventCollection[KeyState.Released] = delegate { };
            keyEventCollection[KeyState.ReleasedThisFrame] = delegate { };
            keyEventCollection[KeyState.Pressed] = delegate { };
            keyEventCollection[KeyState.PressedThisFrame] = delegate { };

            events[command] = keyEventCollection;
            return keyEventCollection;
        }

        public static void Subscribe(KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            keyEventCollection[state] += action;
        }

        public static void Unsubscribe(KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyEventCollection keyEventCollection = GetKeyEventCollection(command);
            keyEventCollection[state] -= action;
        }
    }
}
