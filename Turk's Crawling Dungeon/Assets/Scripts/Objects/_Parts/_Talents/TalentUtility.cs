using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using TCD.Graphics;

namespace TCD.Objects.Parts.Talents
{
    [ContainsConsoleCommand]
    public static class TalentUtility 
    {
        private static List<Type> playerTalents;
        private static Dictionary<string, Type> playerTalentsByName;
        private static Dictionary<Type, Talent> talentInstancesByName = new Dictionary<Type, Talent>();
        private static bool foundPlayerTalents;
        private static StringBuilder stringBuilder = new StringBuilder();

        private static List<Type> PlayerTalents
        {
            get
            {
                if (playerTalents == null)
                {
                    playerTalents = new List<Type>();
                    FindPlayerTalents();
                }
                return playerTalents;
            }
        }

        private static Dictionary<string, Type> PlayerTalentsByName
        {
            get
            {
                if (playerTalentsByName == null)
                {
                    playerTalentsByName = new Dictionary<string, Type>();
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
                if (type.IsSubclassOf(typeof(Talent)))
                { 
                    PlayerTalents.Add(type);
                    PlayerTalentsByName[type.Name] = type;
                }
            }
        }

        public static Type Get(string name)
        {
            try
            {
               return PlayerTalentsByName[name];
            }
            catch
            {
                ExceptionHandler.HandleMessage("Player Talent " + name + " not defined!");
                return default;
            }
        }

        public static Talent GetTalentInstance(Type type)
        {
            if (!talentInstancesByName.TryGetValue(type, out Talent talent))
            {
                GameObject parent = ParentManager.TalentInstances.gameObject;
                talent = (Talent) parent.AddComponent(type);
                talent.enabled = false;
                talentInstancesByName[type] = talent;
            }
            return talent;
        }

        [ConsoleCommand("find_missing_talent_icons")]
        [ConsoleCommand("find_missing_talent_sprites")]
        public static void FindMissingTalentSprites()
        {
            int missingSprites = 0;
            foreach (Type type in PlayerTalents)
            {
                Talent talent = GetTalentInstance(type);
                if (SpriteUtility.IsEmpty(talent.Icon))
                    missingSprites++;
            }
            DebugLogger.Log(missingSprites + " talents reference missing icon sprites."); 
        }

        [ConsoleCommand("addtalent")]
        public static Talent AddTalentToPlayer(string talentName)
        {
            Type type = Get(talentName);
            Talent talent = (Talent) PlayerInfo.currentPlayer.Parts.Add(type);
            return talent;
        }

        public static bool AddTalentPoint(Talent talent)
        {
            if (PlayerInfo.talentPoints <= 0 || talent.level >= talent.MaxLevel)
                return false;
            talent.level++;
            PlayerInfo.talentPoints--;
            EventManager.Send(new TalentPointModifiedEvent(talent, talent.level));
            return true;
        }

        public static bool RemoveTalentPoint(Talent talent)
        {
            if (talent.level == 1)
                return false;
            talent.level--;
            PlayerInfo.talentPoints++;
            EventManager.Send(new TalentPointModifiedEvent(talent, talent.level));
            return true;
        }

        public static string BuildTalentTitle(Talent talent)
        {
            return $"<color=yellow>{talent.Name}</color>";
        }

        public static string BuildTalentDescription(Talent talent, int level, TalentDescriptionStyle style = TalentDescriptionStyle.Tooltip)
        {
            bool isNotTooltip = style != TalentDescriptionStyle.Tooltip;
            bool embark = style == TalentDescriptionStyle.EmbarkView;

            stringBuilder.Clear();
            AppendToDescription($"<color=#039be5>Level:</color> {level}", isNotTooltip);

            if (isNotTooltip)
                AppendRequirements(talent, level, embark);

            switch (talent.UseMode)
            {
                case UseMode.Activated:
                    AppendToDescription("\n<color=#039be5>Use Mode:</color> Activated", isNotTooltip);
                    break;
                case UseMode.Toggle:
                    AppendToDescription("\n<color=#039be5>Use Mode:</color> Toggle", isNotTooltip);
                    break;
                default:
                    AppendToDescription("\n<color=#039be5>Use Mode:</color> Passive", isNotTooltip);
                    break;
            }
            switch (talent.TargetMode)
            {
                case TargetMode.Attack:
                    AppendToDescription("\n<color=#039be5>Target Mode:</color> Attack", isNotTooltip);
                    break;
                case TargetMode.Cell:
                    AppendToDescription("\n<color=#039be5>Target Mode:</color> Cell", isNotTooltip);
                    break;
                case TargetMode.Object:
                    AppendToDescription("\n<color=#039be5>Target Mode:</color> Object", isNotTooltip);
                    break;
                default:
                    break;
            }
            Resource resource = talent.Resource;

            float activationResourceCost = talent.GetActivationResourceCost(level);
            if (activationResourceCost > 0)
                AppendToDescription($"\n<color=#039be5>Activation Cost:</color> {activationResourceCost} {resource}", isNotTooltip);

            float sustainResourceCost = talent.GetSustainResourceCost(level);
            if (sustainResourceCost > 0)
                AppendToDescription($"\n<color=#039be5>Sustain Cost:</color> {sustainResourceCost} {resource}", isNotTooltip);

            int range = talent.GetRange(level);
            if (range <= 1)
                AppendToDescription($"\n<color=#039be5>Range:</color> Melee", isNotTooltip);
            else
                AppendToDescription($"\n<color=#039be5>Range:</color> {range}", isNotTooltip);

            float turnsToUse = ((float)talent.GetEnergyCost() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);
            if (turnsToUse == 0)
                AppendToDescription($"\n<color=#039be5>Turns To Use:</color> Instant (0)", isNotTooltip);
            else
                AppendToDescription($"\n<color=#039be5>Turns To Use:</color> {turnsToUse}", isNotTooltip);

            float cooldown = ((float)talent.GetCooldown(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);
            AppendToDescription($"\n<color=#039be5>Cooldown:</color> {cooldown}", isNotTooltip);
            AppendToDescription($"\n{talent.GetDescription(level)}", false);
            return stringBuilder.ToString();
        }

        private static void AppendToDescription(string append, bool newLines)
        {
            stringBuilder.Append(append);
            if (newLines)
                stringBuilder.Append("\n");
        }

        private static void AppendRequirements(Talent talent, int level, bool embark)
        {
            List<ITalentRequirement> requirements = talent.GetRequirements(level);
            foreach (ITalentRequirement requirement in requirements)
            {
                bool requirementMet = embark ? requirement.MeetsEmbarkRequirement() : requirement.MeetsRequirement();
                string str = requirementMet ? "<color=green>{0}</color>\n" : "<color=red>{0}</color>\n";
                stringBuilder.Append(string.Format(str, requirement.GetDescription()));
            }
            if (requirements.Count > 0)
                stringBuilder.Append("\n");
        }
    }
}
