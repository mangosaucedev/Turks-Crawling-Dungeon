using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Encounters
{
    public class EncounterObject
    {
        public string name;
        public int min;
        public int max;
        public int chanceIn100;
        public bool encounter;
        public bool forced;
        public bool exclusive;

        public List<string> GetObjects()
        {
            List<string> objects = new List<string>();
            int count = RandomInfo.Random.Next(min, max);
            for (int i = 0; i < count; i++)
                objects.Add(name);
            return objects;
        }
    }
}
