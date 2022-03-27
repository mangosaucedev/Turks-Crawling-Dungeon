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
        [SerializeField] private InputField inputField;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Text log;

        private static Dictionary<string, MethodInfo> commandMethods = new Dictionary<string, MethodInfo>();
        private static bool commandMethodsLoaded;

        private void Awake()
        {
            if (!commandMethodsLoaded)
                LoadCommandMethods();

            foreach (string message in DebugLogger.consoleLog)
                log.text += message + "\n";
            scrollbar.value = 1f;
            inputField.onEndEdit.AddListener(OnEndEdit);
            inputField.Select();
        }

        private void OnEnable()
        {
            EventManager.Listen<ConsoleLogEntryAddedEvent>(this, OnConsoleLogEntryAdded);
        }

        private void OnDisable()
        {
            EventManager.StopListening<ConsoleLogEntryAddedEvent>(this);
        }

        private void LoadCommandMethods()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                ContainsConsoleCommandAttribute attribute = type.GetCustomAttribute<ContainsConsoleCommandAttribute>();
                if (attribute != null)
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
                    foreach (MethodInfo method in methods)
                    {
                        IEnumerable<ConsoleCommandAttribute> commandAttributes = method.GetCustomAttributes<ConsoleCommandAttribute>();
                        foreach (ConsoleCommandAttribute commandAttribute in commandAttributes)
                        {
                            if (commandAttribute != null)
                                commandMethods[commandAttribute.command] = method;
                        }
                    }
                }
            }
        }

        private void OnEndEdit(string input)
        {
            string[] split = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                string command = split[0];
                if (!commandMethods.TryGetValue(command, out MethodInfo method))
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
            }
            catch
            {
                DebugLogger.LogError("Could not parse command " + input + "!");
            }
        }

        private void OnConsoleLogEntryAdded(ConsoleLogEntryAddedEvent e)
        {
            string message = e.message;
            log.text += message + "\n";
            scrollbar.value = 1f;
            inputField.Select();
        }
    }
}
