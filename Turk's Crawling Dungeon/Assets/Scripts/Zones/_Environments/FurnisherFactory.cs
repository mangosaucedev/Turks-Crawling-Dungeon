using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public static class FurnisherFactory 
    {
        public static Dictionary<string, Furnisher> furnishersByName = 
            new Dictionary<string, Furnisher>();
        public static List<Furnisher> furnishers = new List<Furnisher>();
    
        public static Furnisher GetFurnisher(params string[] tags)
        {
            return null;
        }

        private static List<Furnisher> GetFurnishersWithTags(string[] tags)
        {
            List<Furnisher> furnishers = new List<Furnisher>();
            return furnishers;
        }
    }
}
