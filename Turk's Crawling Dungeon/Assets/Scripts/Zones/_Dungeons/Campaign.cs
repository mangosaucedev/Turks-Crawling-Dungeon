using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Dungeons
{
    public class Campaign
    {
        public string name;
        public List<string> dungeonNames = new List<string>();
        
        private List<Dungeon> dungeons = new List<Dungeon>();

        public List<Dungeon> Dungeons
        {
            get
            {
                if (dungeons.Count == 0 && dungeonNames.Count > 0)
                {
                    foreach (string dungeonName in dungeonNames)
                        dungeons.Add(Assets.Get<Dungeon>(dungeonName));
                }
                return dungeons;
            }
        }
    }
}
