using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TCD.UI
{
    public class DevConsole : MonoBehaviour
    {
        private const int MAX_MESSAGES = 128;

        [SerializeField] private InputField inputField;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Text log;

        private void Awake()
        {
            DevConsoleUtility.LoadCommandMethods();

            int count = DebugLogger.consoleLog.Count;
            if (count > 0)
            {
                int start = Mathf.Max(DebugLogger.consoleLog.Count - MAX_MESSAGES, 0);
                int end = DebugLogger.consoleLog.Count;
                for (int i = start; i < end; i++)
                {
                    string message = DebugLogger.consoleLog[i];
                    AppendLogEntry(message);
                }
            }
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnEnable()
        {
            EventManager.Listen<ConsoleLogEntryAddedEvent>(this, OnConsoleLogEntryAdded);
        }

        private void OnDisable()
        {
            EventManager.StopListening<ConsoleLogEntryAddedEvent>(this);
        }

        private void OnEndEdit(string input)
        {
            string[] split = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                string command = split[0];
                if (!DevConsoleUtility.commandMethodsByName.TryGetValue(command, out MethodInfo method))
                {
                    DebugLogger.Log("Console command " + command + " not found.");
                    return;
                }
                List<string> arguments = split.ToList();
                if (arguments.Count > 0)
                    arguments.RemoveAt(0);
                string[] args = arguments.ToArray();
                DebugLogger.Log(input);
                method.Invoke(null, args);
                if (ViewManager.IsOpen("Main Menu"))
                    ViewManager.Close("Main Menu");
                ViewManager.Close(name);
            }
            catch
            {
                DebugLogger.LogError("Could not parse command " + input + "!");
            }
        }

        private void OnConsoleLogEntryAdded(ConsoleLogEntryAddedEvent e)
        {
            string message = e.message;
            AppendLogEntry(message);
        }

        private void AppendLogEntry(string message)
        {
            log.text += message + "\n";
            scrollbar.value = 1f;
            inputField.Select();
        }
    }
}
