using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class ZoneEnvironment
    {
        public string name;
        public float weight;
        public EnvironmentPlacement placement;
        public bool forced;
        public bool exclusive;
        public Environment environment;

        public ZoneEnvironment()
        {

        }

        public ZoneEnvironment(string name, float weight, EnvironmentPlacement placement, bool forced, bool exclusive) : this()
        {
            this.name = name;
            this.weight = weight;
            this.placement = placement;
            this.forced = forced;
            this.exclusive = exclusive;
            environment = null;
        }
    }
}
