using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TCD.Inputs;
using Newtonsoft.Json;

namespace TCD.IO
{
    public class KeybindingsLoader
    {
        public enum State
        {
            Version,
            Array
        }

        private string json;
        private JsonTextReader reader;
        private State state;
        private KeyCommand currentCommand;
        private Keybinding currentDefinition;
        private int currentArrayIndex;

        public void TryToLoad()
        {
            if (!File.Exists(KeybindingInfo.Path))
                CreateDefaultKeybindings();
            json = File.ReadAllText(KeybindingInfo.Path);
            SetupReaderAndReadJson();
            DebugLogger.Log($"Loaded keybindings from " + KeybindingInfo.Path);
        }

        private void CreateDefaultKeybindings()
        {
            DefaultKeybindingWriter defaultKeybindingWriter =
                new DefaultKeybindingWriter();
            defaultKeybindingWriter.Write();
        }

        private void SetupReaderAndReadJson()
        {
            using (reader = new JsonTextReader(new StringReader(json)))
            {
                while (reader.Read())
                    ReadTokens();
            }
        }

        private void ReadTokens()
        {
            JsonToken tokenType = reader.TokenType;
            object value = reader.Value;
            if (tokenType == JsonToken.PropertyName)
                ReadProperty((string)value);
            if (tokenType == JsonToken.String)
                ReadString((string)value);
            if (tokenType == JsonToken.StartArray)
                StartArray();
            if (tokenType == JsonToken.EndArray)
                EndKeybinding();
        }

        private void ReadProperty(string propertyName)
        {
            if (propertyName != "Version")
                StartKeyCommand(propertyName);
            else
                state = State.Version;
        }

        private void StartKeyCommand(string keyCommandName)
        {
            currentCommand = (KeyCommand)Enum.Parse(typeof(KeyCommand), keyCommandName);
            currentDefinition = new Keybinding();
        }

        private void ReadString(string str)
        {
            if (state != State.Version)
            {
                KeyCode keyCode = ParseKeyCode(str);
                if (currentArrayIndex == 0)
                    currentDefinition.key = keyCode;
                if (currentArrayIndex == 1)
                    currentDefinition.primaryModifier = keyCode;
                if (currentArrayIndex == 2)
                    currentDefinition.secondaryModifier = keyCode;
                currentArrayIndex++;
            }
            else
                CompareVersion(str);
        }

        private KeyCode ParseKeyCode(string keyCodeName) =>
            (KeyCode)Enum.Parse(typeof(KeyCode), keyCodeName);

        private void CompareVersion(string version)
        {
            DebugLogger.Log($"Keybindings version: {version}.");
        }

        private void StartArray()
        {
            state = State.Array;
            currentArrayIndex = 0;
        }

        private void EndKeybinding() => 
            Keys.AddKeybinding(currentCommand, currentDefinition);
    }
}
