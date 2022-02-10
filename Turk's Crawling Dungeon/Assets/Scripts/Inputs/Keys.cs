using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public static class Keys 
    {
        public static Dictionary<KeyCommand, Keybinding> keybindings = 
            new Dictionary<KeyCommand, Keybinding>();
        public static Dictionary<KeyCode, List<KeyCommand>> keys =
            new Dictionary<KeyCode, List<KeyCommand>>();
        private static KeyCommand currentCommand;
        private static KeyState currentState;
        private static Keybinding currentKeybinding;

        static Keys()
        {
            foreach (KeyCommand command in Enum.GetValues(typeof(KeyCommand)))
                keybindings[command] = new Keybinding(KeyCode.None, KeyCode.None, KeyCode.None);
        }

        public static bool GetCommand(KeyCommand command, KeyState state)
        {
            currentCommand = command;
            currentState = state;
            currentKeybinding = TryGetKeybinding(currentCommand);
            bool validModifiers = ModifiersPressed() || !CurrentStateIsPressed();
            if (validModifiers && currentState == GetCommandState())
                return !GetCommandConflicts(command);
            return false;
        }

        private static bool ModifiersPressed()
        {
            KeyCode primaryModifier = currentKeybinding.primaryModifier;
            KeyCode secondaryModifier = currentKeybinding.secondaryModifier;
            bool primaryModifierPressed =
                primaryModifier == KeyCode.None || Input.GetKey(primaryModifier);
            bool secondaryModifierPressed =
                secondaryModifier == KeyCode.None || Input.GetKey(secondaryModifier);
            return primaryModifierPressed && secondaryModifierPressed;
        }

        private static bool CurrentStateIsPressed() =>
            currentState == KeyState.Pressed || currentState == KeyState.PressedThisFrame;

        private static KeyState GetCommandState()
        {
            KeyCode key = currentKeybinding.key;
            if (Input.GetKeyDown(key))
                return KeyState.PressedThisFrame;
            if (Input.GetKey(key))
                return KeyState.Pressed;
            if (Input.GetKeyUp(key))
                return KeyState.ReleasedThisFrame;
            return KeyState.Released;
        }

        public static Keybinding TryGetKeybinding(KeyCommand command)
        {
            try
            {
                return keybindings[command];
            }
            catch
            {
                ExceptionHandler.Handle(new KeyException($"Command {command} is unbound!"));
            }
            return null;
        }

        private static bool GetCommandConflicts(KeyCommand command)
        {
            KeyCode key = currentKeybinding.key;
            KeyCode primaryModifier = currentKeybinding.primaryModifier;
            KeyCode secondaryModifier = currentKeybinding.secondaryModifier;
            List<KeyCommand> commandsBoundToKey = GetKeyCodeList(key);
            foreach (KeyCommand otherCommand in commandsBoundToKey)
            {
                if (command == otherCommand)
                    continue;
                Keybinding keybinding = keybindings[otherCommand];
                KeyCode otherPrimaryModifier = keybinding.primaryModifier;
                KeyCode otherSecondaryModifier = keybinding.secondaryModifier;

                if (primaryModifier == KeyCode.None && otherPrimaryModifier != KeyCode.None && Input.GetKey(otherPrimaryModifier))
                    return true;
                else if (primaryModifier != KeyCode.None && otherPrimaryModifier != KeyCode.None)
                {
                    if (secondaryModifier == KeyCode.None && otherSecondaryModifier != KeyCode.None && Input.GetKey(otherSecondaryModifier))
                        return true;
                }
            }
            return false;
        }

        public static void AddKeybinding(KeyCommand command, Keybinding keybinding)
        {
            Keys.keybindings[command] = keybinding;
            KeyCode key = keybinding.key;
            List<KeyCommand> commandsBoundToKey = GetKeyCodeList(key);
            commandsBoundToKey.Add(command);
#if UNITY_EDITOR
            KeyEventManager.keybinds.Add(new KeyEventManager.Keybind(
                command,
                key,
                keybinding.primaryModifier,
                keybinding.secondaryModifier));
#endif
        }

        private static List<KeyCommand> GetKeyCodeList(KeyCode key)
        {
            if (!keys.TryGetValue(key, out List<KeyCommand> list))
            {
                list = new List<KeyCommand>();
                keys[key] = list;
            }
            return list;
        }
    }
}
