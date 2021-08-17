using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.Zones
{
    public class ZoneEncounter
    {
        public string name;
        public float weight;
        public EncounterType type;
        public bool forced;
        public bool exclusive;
    }
}