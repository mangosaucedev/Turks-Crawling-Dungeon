using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class FurnishingCollection
    {
        private GrabBag<Furnishing> furnishings = new GrabBag<Furnishing>();
        private string chosenFurnishing;

        public void Add(Furnishing furnishing) => furnishings.AddItem(furnishing, furnishing.weight);

        public Furnishing GetStatic()
        {
            Furnishing furnishing = null;
            if (string.IsNullOrEmpty(chosenFurnishing))
            {
                furnishing = GetRandom();
                chosenFurnishing = furnishing.name;
                return furnishing;
            }
            if (furnishings.TryGet(f => f.name == chosenFurnishing, out furnishing))
                return furnishing;
            return null;
        }

        public Furnishing GetRandom() => furnishings.Grab();
        
    }
}
