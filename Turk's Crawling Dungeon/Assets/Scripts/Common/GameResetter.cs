using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TCD.Zones;

namespace TCD
{
    public static class GameResetter
    {
        private static Type currentType;

        public static void ResetGame()
        {
            DebugLogger.Log("RESETTING GAME!");
            ResetGameStatics();
            DebugLogger.Log("GAME RESET COMPLETE.");
            ZoneResetter zoneResetter = ServiceLocator.Get<ZoneResetter>();
            zoneResetter.resetPlayer = true;
        }

        private static void ResetGameStatics()
        {
            Type[] types = GetListOfTypes();
            foreach (Type type in types)
                ResetGameStaticsInType(type);
        }

        private static Type[] GetListOfTypes()
        {
            Assembly assembly = typeof(GameResetter).Assembly;
            return assembly.GetTypes();
        }

        private static void ResetGameStaticsInType(Type type)
        {
            if (type.GetCustomAttribute<ContainsGameStaticsAttribute>() != null)
            {
                currentType = type;
                FieldInfo[] staticFields = currentType.GetFields(BindingFlags.Public | BindingFlags.Static);
                //DebugLogger.Log($"\t{currentType.Name} contains {staticFields.Length} static fields.");
                foreach (FieldInfo staticField in staticFields)
                    ResetGameStaticsForField(staticField);
            }
        }

        private static void ResetGameStaticsForField(FieldInfo field)
        {
            GameStaticAttribute attribute = field.GetCustomAttribute<GameStaticAttribute>();
            if (attribute != null)
            {
                field.SetValue(null, attribute.value);
                //DebugLogger.Log($"\t\tResetting {currentType.Name}'s field \"{field.Name}\" to {attribute.value}");
            }
        }
    }
}
