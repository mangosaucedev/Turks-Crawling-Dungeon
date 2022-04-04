using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TCD.Graphics;

namespace TCD.Objects.Parts.Talents
{
    [ContainsConsoleCommand]
    public static class PlayerTalentUtility 
    {
        private static List<PlayerTalentEntry> playerTalents;
        private static Dictionary<string, PlayerTalentEntry> playerTalentsByName;
        private static Dictionary<string, Talent> talentInstancesByName = new Dictionary<string, Talent>();
        private static bool foundPlayerTalents;

        private static List<PlayerTalentEntry> PlayerTalents
        {
            get
            {
                if (playerTalents == null)
                {
                    playerTalents = new List<PlayerTalentEntry>();
                    FindPlayerTalents();
                }
                return playerTalents;
            }
        }

        private static Dictionary<string, PlayerTalentEntry> PlayerTalentsByName
        {
            get
            {
                if (playerTalentsByName == null)
                {
                    playerTalentsByName = new Dictionary<string, PlayerTalentEntry>();
                    FindPlayerTalents();
                }
                return playerTalentsByName;
            }
        }

        private static void FindPlayerTalents()
        {
            if (foundPlayerTalents)
                return;
            foundPlayerTalents = true;
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
                    PlayerTalents.Add(entry);
                    PlayerTalentsByName[entry.name] = entry;
                }
            }
        }

        public static PlayerTalentEntry Get(string name)
        {
            try
            {
               return PlayerTalentsByName[name];
            }
            catch
            {
                ExceptionHandler.HandleMessage("Player Talent " + name + " not defined! Are you sure the associated class has PlayerTalentAttribute?");
                return default;
            }
        }

        public static Talent GetTalentInstance(string name)
        {
            if (!talentInstancesByName.TryGetValue(name, out Talent talent))
            {
                GameObject parent = ParentManager.TalentInstances.gameObject;
                PlayerTalentEntry entry = Get(name); 
                talent = (Talent) parent.AddComponent(entry.type);
                talent.enabled = false;
                talentInstancesByName[name] = talent;
            }
            return talent;
        }

        [ConsoleCommand("find_missing_talent_icons")]
        [ConsoleCommand("find_missing_talent_sprites")]
        public static void FindMissingTalentSprites()
        {
            int missingSprites = 0;
            foreach (PlayerTalentEntry playerTalent in PlayerTalents)
            {
                Talent talent = GetTalentInstance(playerTalent.name);
                if (SpriteUtility.IsEmpty(talent.Icon))
                    missingSprites++;
            }
            DebugLogger.Log(missingSprites + " talents reference missing icon sprites."); 
        }
    }
}
