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
        private static int STARTING_TALENT_POINTS = 4;

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
            PlayerInfo.talentPoints = STARTING_TALENT_POINTS;
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
            if (PlayerInfo.talentPoints == 0)
                return;
            if (!chosenTalents.Contains(talent))
                chosenTalents.Add(talent);
            if (!chosenTalentLevels.ContainsKey(talent))
                chosenTalentLevels[talent] = 0;
            chosenTalentLevels[talent] += 1;
            PlayerInfo.talentPoints--;
            EventManager.Send(new EmbarkTalentPointModifiedEvent(TalentUtility.Get(talent), chosenTalentLevels[talent]));
        }

        public static void RemoveChosenTalentLevel(string talent)
        {
            if (!chosenTalentLevels.ContainsKey(talent))
                return;
            chosenTalentLevels[talent] -= 1;
            PlayerInfo.talentPoints++;
            if (chosenTalentLevels[talent] == 0)
            {
                chosenTalentLevels.Remove(talent);
                chosenTalents.Remove(talent);
            }
            EventManager.Send(new EmbarkTalentPointModifiedEvent(TalentUtility.Get(talent), GetChosenTalentLevel(talent)));
        }

        public static int GetChosenTalentLevel(string talent)
        {
            if (!chosenTalentLevels.ContainsKey(talent))
                return 0;
            return chosenTalentLevels[talent];
        }
    }
}
