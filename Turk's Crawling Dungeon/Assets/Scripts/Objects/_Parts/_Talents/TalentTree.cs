using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TCD.Objects.Parts.Talents
{
    public class TalentTree 
    {
        public string name;
        public string displayName;
        public List<string> talentNames = new List<string>();

        private List<Talent> talents = new List<Talent>();

        public List<Talent> Talents
        {
            get
            {
                if (talents.Count == 0 && talentNames.Count > 0)
                {
                    foreach (string talentName in talentNames)
                    {
                        Talent talent = PlayerTalentUtility.GetTalentInstance(talentName);
                        talents.Add(talent);
                    }   
                }
                return talents;
            }
        } 
    }
}
