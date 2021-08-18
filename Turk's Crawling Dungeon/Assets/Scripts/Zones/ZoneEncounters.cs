using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.Zones
{
    public class ZoneEncounters : ICloneable
    {
        public float density;
        public List<ZoneEncounter> buildEncounters = new List<ZoneEncounter>();
        public List<Encounter> encounters = new List<Encounter>();
        public Dictionary<Encounter, bool> placedEncounters = new Dictionary<Encounter, bool>();

        public void BuildEncounters()
        {
            foreach (ZoneEncounter e in buildEncounters)
            {
                Encounter encounter = EncounterFactory.BuildFromBlueprint(e.name);
                encounter.weight = e.weight;
                encounter.type = e.type;
                encounter.forced = e.forced;
                encounter.exclusive = e.exclusive;
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
                    int maxTier = GetMaxTier();
                    bool isMaxTier = (encounter.tier == maxTier && tier > maxTier);
                    if (encounter.type == type && (encounter.tier == tier || isMaxTier))
                        bag.AddItem(encounter, encounter.weight);
                }
                return bag.Grab();
            }
        }

        private int GetMaxTier()
        {
            int tier = 0;
            foreach (Encounter encounter in encounters)
            {
                if (encounter.tier > tier)
                    tier = encounter.tier;
            }
            return tier;
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
            for (int i = encounters.Count - 1; i >= 0; i--)
            {
                Encounter encounter = encounters[i];
                zoneEncounters.encounters[i] = encounter;
            }
            return zoneEncounters;
        }
    }
}
