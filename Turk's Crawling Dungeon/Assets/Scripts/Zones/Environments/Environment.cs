using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class Environment
    {
        public string name;
        public float weight;
        public EnvironmentPlacement placement;
        public bool exclusive;
        public bool forced;
        public float furnishingDensity;
        public List<Furnisher> furnishers;
        public List<Furnishing> furnishings = new List<Furnishing>();
        private Dictionary<string, FurnishingCollection> categorizedFurnishings =
            new Dictionary<string, FurnishingCollection>();
        private Dictionary<Furnisher, bool> usedFurnishers = new Dictionary<Furnisher, bool>();

        public void AddFurnishing(string category, Furnishing furnishing)
        {
            FurnishingCollection collection = GetCategorizedFurnishingCollection(category);
            collection.Add(furnishing);
        }

        private FurnishingCollection GetCategorizedFurnishingCollection(string category)
        {
            if (!categorizedFurnishings.TryGetValue(category, out FurnishingCollection collection))
            {
                collection = new FurnishingCollection();
                categorizedFurnishings[category] = collection;
            }
            return collection;
        }

        public string GetRandomFurnishing(string category)
        {
            FurnishingCollection collection = GetCategorizedFurnishingCollection(category);
            Furnishing random = collection.GetRandom();
            return random.name;
        }

        public string GetStaticFurnishing(string category)
        {
            FurnishingCollection collection = GetCategorizedFurnishingCollection(category);
            Furnishing furnishing = collection.GetStatic();
            return furnishing.name;
        }

        public List<Furnisher> GetForcedFurnishers()
        {
            List<Furnisher> forcedFurnishers = new List<Furnisher>();
            foreach (Furnisher furnisher in furnishers)
            {
                if (furnisher.forced)
                    forcedFurnishers.Add(furnisher);
            }
            return forcedFurnishers;
        }

        public List<Furnisher> GetNonExcludedFurnishers()
        {
            List<Furnisher> nonExcludedFurnishers = new List<Furnisher>();
            foreach (Furnisher furnisher in furnishers)
            {
                if (!furnisher.exclusive || !FurnisherWasUsed(furnisher))
                    nonExcludedFurnishers.Add(furnisher);
            }
            return nonExcludedFurnishers;
        }

        private bool FurnisherWasUsed(Furnisher furnisher)
        {
            if (!usedFurnishers.TryGetValue(furnisher, out bool used))
            {
                used = false;
                usedFurnishers[furnisher] = used;
            }
            return used;
        }

        public void FlagFurnisherAsUsed(Furnisher furnisher) => usedFurnishers[furnisher] = true;
    }
}
