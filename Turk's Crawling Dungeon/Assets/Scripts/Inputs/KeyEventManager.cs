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
        private static Dictionary<InputGroup, KeyCommandEventCollection> groupedEvents =
            new Dictionary<InputGroup, KeyCommandEventCollection>();

#if UNITY_EDITOR
        public static List<Keybind> keybinds = new List<Keybind>();
        
        [SerializeField] private List<Keybind> loadedKeybindings;
#endif

        private void Update()
        {
            foreach (InputGroup group in Enum.GetValues(typeof(InputGroup)))
            {
                KeyCommandEventCollection eventGroup = GetGroupedEvents(group);
                if (eventGroup.isEnabled)
                {
                    foreach (KeyCommand command in Enum.GetValues(typeof(KeyCommand)))
                        eventGroup.CheckKeyCommandState(command);
                }
            }

#if UNITY_EDITOR
            loadedKeybindings = keybinds;
#endif
        }

        private static KeyCommandEventCollection GetGroupedEvents(InputGroup group)
        {
            if (!groupedEvents.TryGetValue(group, out KeyCommandEventCollection eventGroup))
            {
                eventGroup = new KeyCommandEventCollection();
                groupedEvents[group] = eventGroup;
            }
            return eventGroup;
        }

        public static bool GetInputGroupEnabled(InputGroup group) =>
            GetGroupedEvents(group).isEnabled;

        public static void SetInputGroupEnabled(InputGroup group, bool isEnabled = true)
        {
            KeyCommandEventCollection eventGroup = GetGroupedEvents(group);
            eventGroup.isEnabled = isEnabled;
        }

        public static void Subscribe(InputGroup group, KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyCommandEventCollection eventGroup = GetGroupedEvents(group);
            eventGroup.Subscribe(command, state, action);
        }

        public static void Unsubscribe(InputGroup group, KeyCommand command, KeyState state, KeyEventDelegate action)
        {
            KeyCommandEventCollection eventGroup = GetGroupedEvents(group);
            eventGroup.Unsubscribe(command, state, action);
        }
    }
}
