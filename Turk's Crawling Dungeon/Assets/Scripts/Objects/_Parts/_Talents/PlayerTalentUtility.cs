using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public static class PlayerTalentUtility 
    {
        private static List<PlayerTalentEntry> playerTalents = new List<PlayerTalentEntry>();
        private static Dictionary<string, PlayerTalentEntry> playerTalentsByName = new Dictionary<string, PlayerTalentEntry>();

        public static List<PlayerTalentEntry> GetPlayerTalents()
        {
            if (playerTalents.Count == 0)
                FindPlayerTalents();
            return playerTalents;
        }

        private static void FindPlayerTalents()
        {
            foreach (Assembly assembly in AssemblyInfo.executingAssemblies)
                AddPlayerTalentsFromTypes(assembly.GetTypes());
        }

        private static void AddPlayerTalentsFromTypes(Type[] types)
        {
            foreach (Type type in types)
            {
                PlayerTalentAttribute attribute = type.GetCustomAttribute<PlayerTalentAttribute>();
                if (attribute != null)
                {
                    PlayerTalentEntry entry = new PlayerTalentEntry(type, attribute.name);
                    playerTalents.Add(entry);
                    playerTalentsByName[entry.name] = entry;
                }
            }
        }

        public static PlayerTalentEntry Get(string name)
        {
            if (playerTalents.Count == 0)
                FindPlayerTalents();
            try
            {
               return playerTalentsByName[name];
            }
            catch
            {
                ExceptionHandler.HandleMessage("Player Talent " + name + " not defined! Are you sure the associated class has PlayerTalentAttribute?");
                return default;
            }
        }
    }
}
