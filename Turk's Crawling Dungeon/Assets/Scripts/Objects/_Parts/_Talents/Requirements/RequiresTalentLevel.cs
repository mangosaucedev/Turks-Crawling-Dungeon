using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public class RequiresTalentLevel : ITalentRequirement
    {
        private string talentName;
        private int level;
        private Type type;

        public RequiresTalentLevel(string talentName, int level = 1)
        {
            this.talentName = talentName;
            this.level = level;
            type = TalentUtility.Get(talentName);
        }

        public RequiresTalentLevel(Type type, int level = 1)
        {
            this.type = type;
            this.level = level;
            talentName = type.Name;
        }

        public bool MeetsRequirement()
        {
            if (PlayerInfo.currentPlayer && 
                PlayerInfo.currentPlayer.Parts.TryGet<Talent>(type, out Talent talent))
                return talent.level >= level;
            return false;
        }

        public bool MeetsEmbarkRequirement() =>
            Embark.GetChosenTalentLevel(talentName) >= level;

        public string GetDescription()
        {
            string description = $"Requires {TalentUtility.GetTalentInstance(type).Name}";
            if (level > 1)
                description += $" level {level}";
            return description;
        }
    }
}
