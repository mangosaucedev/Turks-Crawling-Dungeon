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
            Add(KeyCommand.Rest, KeyCode.Z);
            Add(KeyCommand.Look, KeyCode.L);
            Add(KeyCommand.Throw, KeyCode.T);
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
            Add(KeyCommand.Hotbar0, KeyCode.Alpha0);
            Add(KeyCommand.Hotbar1, KeyCode.Alpha1);
            Add(KeyCommand.Hotbar2, KeyCode.Alpha2);
            Add(KeyCommand.Hotbar3, KeyCode.Alpha3);
            Add(KeyCommand.Hotbar4, KeyCode.Alpha4);
            Add(KeyCommand.Hotbar5, KeyCode.Alpha5);
            Add(KeyCommand.Hotbar6, KeyCode.Alpha6);
            Add(KeyCommand.Hotbar7, KeyCode.Alpha7);
            Add(KeyCommand.Hotbar8, KeyCode.Alpha8);
            Add(KeyCommand.Hotbar9, KeyCode.Alpha9);
            Add(KeyCommand.HotbarC0, KeyCode.Alpha0, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC1, KeyCode.Alpha1, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC2, KeyCode.Alpha2, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC3, KeyCode.Alpha3, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC4, KeyCode.Alpha4, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC5, KeyCode.Alpha5, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC6, KeyCode.Alpha6, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC7, KeyCode.Alpha7, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC8, KeyCode.Alpha8, KeyCode.LeftControl);
            Add(KeyCommand.HotbarC9, KeyCode.Alpha9, KeyCode.LeftControl);
            Add(KeyCommand.HotbarS0, KeyCode.Alpha0, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS1, KeyCode.Alpha1, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS2, KeyCode.Alpha2, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS3, KeyCode.Alpha3, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS4, KeyCode.Alpha4, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS5, KeyCode.Alpha5, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS6, KeyCode.Alpha6, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS7, KeyCode.Alpha7, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS8, KeyCode.Alpha8, KeyCode.LeftShift);
            Add(KeyCommand.HotbarS9, KeyCode.Alpha9, KeyCode.LeftShift);
            //
            Add(KeyCommand.Reset, KeyCode.R);
            Add(KeyCommand.ResetToDefaults, KeyCode.R, KeyCode.LeftShift);
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
