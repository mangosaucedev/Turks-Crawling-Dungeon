using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Zones.Dungeons
{
    public class Campaign
    {
        public string name;
        public string description;
        public bool hideSelection;
        public List<string> dungeonNames = new List<string>();
        public List<string> classNames = new List<string>();

        private List<Dungeon> dungeons = new List<Dungeon>();
        private List<Class> classes = new List<Class>();

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

        public List<Class> Classes
        {
            get
            {
                if (classes.Count == 0 && classNames.Count > 0)
                {
                    foreach (string className in classNames)
                        classes.Add(Assets.Get<Class>(className));
                }
                return classes;
            }
        }
    }
}
