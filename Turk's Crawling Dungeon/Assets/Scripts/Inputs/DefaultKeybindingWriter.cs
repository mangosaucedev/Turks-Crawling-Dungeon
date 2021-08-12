using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace TCD.Inputs
{
    public class DefaultKeybindingWriter
    {
        private static StringWriter stringWriter = new StringWriter();
        private static Dictionary<KeyCommand, Keybinding> defaultKeybindings = 
            new Dictionary<KeyCommand, Keybinding>();

        static DefaultKeybindingWriter()
        {
            Add(KeyCommand.Cancel, KeyCode.Escape);
            Add(KeyCommand.Enter, KeyCode.Space);
            Add(KeyCommand.MoveNorthwest, KeyCode.Keypad7);
            Add(KeyCommand.MoveNorth, KeyCode.Keypad8);
            Add(KeyCommand.MoveNortheast, KeyCode.Keypad9);
            Add(KeyCommand.MoveWest, KeyCode.Keypad4);
            Add(KeyCommand.MovePass, KeyCode.Keypad5);
            Add(KeyCommand.MoveEast, KeyCode.Keypad6);
            Add(KeyCommand.MoveSouthwest, KeyCode.Keypad1);
            Add(KeyCommand.MoveSouth, KeyCode.Keypad2);
            Add(KeyCommand.MoveSoutheast, KeyCode.Keypad3);
            Add(KeyCommand.Interact, KeyCode.Space);
            Add(KeyCommand.InteractAdvanced, KeyCode.Space, KeyCode.LeftShift);
            Add(KeyCommand.OpenInventory, KeyCode.I);
            Add(KeyCommand.OpenHealth, KeyCode.H);
            Add(KeyCommand.OpenHelp, KeyCode.Slash, KeyCode.LeftShift);
            Add(KeyCommand.OpenStatus, KeyCode.Z);
            // TODO - Delete these later after 8/13/21
            Add(KeyCommand.MoveNorthAlt, KeyCode.UpArrow);
            Add(KeyCommand.MoveSouthAlt, KeyCode.DownArrow);
            Add(KeyCommand.MoveWestAlt, KeyCode.LeftArrow);
            Add(KeyCommand.MoveEastAlt, KeyCode.RightArrow);
            //
        }

        private static void Add(KeyCommand command, KeyCode key, KeyCode primaryModifier = KeyCode.None, 
            KeyCode secondaryModifier = KeyCode.None) =>
            defaultKeybindings[command] = new Keybinding(key, primaryModifier, secondaryModifier);

        public void Write()
        {
            if (File.Exists(KeybindingInfo.Path))
            {
                DebugLogger.Log($"Default keybindings already located at " + KeybindingInfo.Path);
                return;
            }
            using (FileStream file = File.Create(KeybindingInfo.Path))
                WriteFile(file);
            DebugLogger.Log($"Default keybindings written to " + KeybindingInfo.Path);
        }

        private void WriteFile(FileStream file)
        {
            using (JsonWriter writer = new JsonTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Version");
                writer.WriteValue(VersionInfo.GetVersionName());
                foreach (KeyCommand command in Enum.GetValues(typeof(KeyCommand)))
                {
                    Keybinding keybinding;
                    if (!defaultKeybindings.TryGetValue(command, out keybinding))
                        keybinding = new Keybinding();
                    writer.WritePropertyName(command.ToString());
                    writer.WriteStartArray();
                    writer.WriteValue(keybinding.key.ToString());
                    writer.WriteValue(keybinding.primaryModifier.ToString());
                    writer.WriteValue(keybinding.secondaryModifier.ToString());
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
                using (StreamWriter streamWriter = new StreamWriter(file))
                    streamWriter.WriteLine(stringWriter.ToString());
            }
        }
    }
}
