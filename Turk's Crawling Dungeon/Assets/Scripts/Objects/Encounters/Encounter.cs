using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Encounters
{
    public class Encounter : ICloneable
    {
        public string name;
        public int tier;
        public float weight;
        public EncounterType type;
        public EncounterDensity density;
        public bool forced;
        public bool exclusive;
        public List<EncounterObject> objects = new List<EncounterObject>();

        public object Clone()
        {
            Encounter encounter = (Encounter) MemberwiseClone();
            foreach (EncounterObject obj in objects)
                encounter.objects.Add(obj);
            return encounter;
        }
    }
}
