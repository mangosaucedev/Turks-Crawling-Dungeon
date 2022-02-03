using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Dungeons
{
    public class Dungeon 
    {
        public string name;
        public List<string> zoneNames = new List<string>();

        private List<IZone> zones = new List<IZone>();

        public List<IZone> Zones
        {
            get
            {
                if (zones.Count == 0 && zoneNames.Count > 0)
                {
                    foreach (string zoneName in zoneNames)
                        zones.Add(ZoneFactory.BuildFromBlueprint(zoneName));
                }
                return zones;
            }
        }
    }
}
