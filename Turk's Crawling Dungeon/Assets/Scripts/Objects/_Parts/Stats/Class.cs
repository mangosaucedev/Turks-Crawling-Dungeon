using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.Objects.Parts
{
    public class Class
    {
        public string name;
        public string description;
        public string statPresetName;
        public List<string> talentTreeNames = new List<string>();

        private StatPreset statPreset;
        private List<TalentTree> talentTrees = new List<TalentTree>();

        public StatPreset StatPreset
        {
            get
            {
                if (statPreset == null)
                    statPreset = Assets.Get<StatPreset>(statPresetName);
                return statPreset;
            }
        }

        public List<TalentTree> TalentTrees
        {
            get
            {
                if (talentTrees.Count == 0 && talentTreeNames.Count > 0)
                {
                    foreach (string talentTreeName in talentTreeNames)
                        talentTrees.Add(Assets.Get<TalentTree>(talentTreeName));
                }
                return talentTrees;
            }
        }
    }
}
