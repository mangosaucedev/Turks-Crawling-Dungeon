using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;

namespace TCD.Objects
{
    public static class PlayerUtility 
    {
        public static BaseObject BuildPlayer(string blueprintName, Vector2Int position)
        {
            BaseObject player = ObjectFactory.BuildFromBlueprint(blueprintName, position);
            PlayerInfo.currentPlayer = player;
            ApplyEmbarkProfile();
            return player;
        }

        public static void ApplyEmbarkProfile()
        {
            AppendTalents();
        }

        private static void AppendTalents()
        {
            foreach (string talentName in Embark.ChosenTalents)
            {
                int level = Embark.ChosenTalentLevels[talentName];
                if (level == 0)
                    continue;
                Talent talent = TalentUtility.AddTalentToPlayer(talentName);
                talent.Level = level;
            }
        }
    }
}
