using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Level : Part
    {
        private int charLevel = 1;
        private long experience;

        public override string Name => "Level";
    
        public int CharLevel
        {
            get => charLevel;
            set => charLevel = value;
        }

        public long Experience => experience;

        public long AddExperience(long experience)
        {
            this.experience += experience;
            if (this.experience > GetExperienceToNextLevel(charLevel))
                LevelUp();
            return this.experience;
        }

        public long GetExperienceToNextLevel(int level)
        {
            return (50L * level) + (long) Mathf.Pow(10f, 1 + level / 10);
        }

        private void LevelUp()
        {
            charLevel++;
        }
    }
}
