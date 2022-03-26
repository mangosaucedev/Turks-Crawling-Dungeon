using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;
using TCD.Zones.Dungeons;

namespace TCD
{
    public static class Embark
    {
        public static bool tutorialEnabled;

        private static Campaign chosenCampaign;
        private static Class chosenClass;
        private static HashSet<string> chosenTalents = new HashSet<string>();
        private static Dictionary<string, int> chosenTalentLevels = new Dictionary<string, int>();

        public static Campaign ChosenCampaign => chosenCampaign;

        public static Class ChosenClass => chosenClass;

        public static HashSet<string> ChosenTalents => chosenTalents;

        public static Dictionary<string, int> ChosenTalentLevels => chosenTalentLevels;

        public static void Reset()
        {
            tutorialEnabled = false;
            chosenCampaign = null;
            chosenClass = null;
            chosenTalents.Clear();
            chosenTalentLevels.Clear();
        }

        public static void SetChosenCampaign(Campaign campaign)
        {
            chosenCampaign = campaign;
        }

        public static void SetChosenClass(Class c)
        {
            chosenClass = c;
            chosenTalents.Clear();
            chosenTalentLevels.Clear();
        }

        public static void AddChosenTalentLevel(string talent)
        {
            if (!chosenTalents.Contains(talent))
                chosenTalents.Add(talent);
            if (!chosenTalentLevels.ContainsKey(talent))
                chosenTalentLevels[talent] = 0;
            chosenTalentLevels[talent] += 1;
        }

        public static void RemoveChosenTalentLevel(string talent)
        {
            if (!chosenTalentLevels.ContainsKey(talent))
                return;
            chosenTalentLevels[talent] -= 1;
            if (chosenTalentLevels[talent] == 0)
            {
                chosenTalentLevels.Remove(talent);
                chosenTalents.Remove(talent);
            }
        }
    }
}
