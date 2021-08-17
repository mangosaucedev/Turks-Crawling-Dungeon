using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.Zones
{
    public class ZoneEncounters : ICloneable
    {
        public List<string> buildEncounters = new List<string>();
        public List<Encounter> encounters = new List<Encounter>();
        public Dictionary<Encounter, bool> placedEncounters = new Dictionary<Encounter, bool>();

        public void BuildEncounters()
        {
            foreach (string e in buildEncounters)
            {
                Encounter encounter = (Encounter) Assets.Get<Encounter>(e).Clone();
                encounters.Add(encounter);
            }
        }

        public Encounter GetEncounter(EncounterType type, int tier = 0)
        {
            List<Encounter> nonExcludedEncounters = GetNonExcludedEncounters();
            using (GrabBag<Encounter> bag = new GrabBag<Encounter>())
            {
                foreach (Encounter encounter in nonExcludedEncounters)
                {
                    if (encounter.type == type && encounter.tier == tier)
                        bag.AddItem(encounter, encounter.weight);
                }
                return bag.Grab();
            }
        }

        public List<Encounter> GetForcedEncounters()
        {
            List<Encounter> forcedEncounters = new List<Encounter>();
            foreach (Encounter encounter in encounters)
            { 
                if (encounter.forced)
                    forcedEncounters.Add(encounter);
            }
            return forcedEncounters;
        }

        private List<Encounter> GetNonExcludedEncounters()
        {
            List<Encounter> nonExcludedEncounters = new List<Encounter>();
            foreach (Encounter encounter in encounters)
            {
                if (!encounter.exclusive || !EncounterWasPlaced(encounter))
                    nonExcludedEncounters.Add(encounter);
            }
            return nonExcludedEncounters;
        }

        private bool EncounterWasPlaced(Encounter encounter)
        {
            if (!placedEncounters.TryGetValue(encounter, out bool placed))
            {
                placed = false;
                placedEncounters[encounter] = placed;
            }
            return placed;
        }

        public void FlagEncounterAsPlaced(Encounter encounter) => placedEncounters[encounter] = true;
        

        public object Clone()
        {
            ZoneEncounters zoneEncounters = (ZoneEncounters) MemberwiseClone();
            foreach (Encounter encounter in encounters)
                zoneEncounters.encounters.Add(encounter);
            return zoneEncounters;
        }
    }
}
