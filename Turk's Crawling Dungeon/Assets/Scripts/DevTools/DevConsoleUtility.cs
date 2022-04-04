using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TCD
{
    [ContainsConsoleCommand]
    public static class DevConsoleUtility
    {
        public static HashSet<string> commands = new HashSet<string>();
        public static Dictionary<string, MethodInfo> commandMethodsByName = new Dictionary<string, MethodInfo>();
        
        private static bool commandMethodsLoaded;

        public static void LoadCommandMethods()
        {
            if (commandMethodsLoaded)
                return;
            commandMethodsLoaded = true;

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
                            {
                                commands.Add(commandAttribute.command);
                                commandMethodsByName[commandAttribute.command] = method;
                            }
                        }
                    }
                }
            }
        }

        [ConsoleCommand("help")]
        [ConsoleCommand("commands")]
        public static void Help()
        {
            DebugLogger.Log("--- CONSOLE COMMANDS: ---");
            foreach (string command in commands)
                DebugLogger.Log("\t" + command);
            DebugLogger.Log("----------------------------------------");
        }
    }
}
