using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class ZoneEnvironments
    {
        public List<Environment> environments = new List<Environment>();
        public List<ZoneEnvironment> environmentReferences = new List<ZoneEnvironment>();
        private Dictionary<Environment, bool> hasBeenPlaced = new Dictionary<Environment, bool>();

        public void AddEnvironmentReference(string name, float weight, EnvironmentPlacement placement, bool forced, bool exclusive)
        {
            ZoneEnvironment zoneEnvironment = new ZoneEnvironment(name, weight, placement, forced, exclusive);
            environmentReferences.Add(zoneEnvironment);
        }

        public List<ZoneEnvironment> GetUnplacedForcedZoneEnvironments()
        {
            List<ZoneEnvironment> unplacedForcedZoneEnvironments = new List<ZoneEnvironment>();
            foreach (ZoneEnvironment zoneEnvironment in environmentReferences)
            {
                Environment environment = zoneEnvironment.environment;
                if (zoneEnvironment.forced && !GetPlaced(environment))
                    unplacedForcedZoneEnvironments.Add(zoneEnvironment);
            }
            return unplacedForcedZoneEnvironments;
        }

        public List<ZoneEnvironment> GetNonExludedZoneEnvironments()
        {
            List<ZoneEnvironment> nonExludedZoneEnvironments = new List<ZoneEnvironment>();
            foreach (ZoneEnvironment zoneEnvironment in environmentReferences)
            {
                Environment environment = zoneEnvironment.environment;
                if (!zoneEnvironment.exclusive || !GetPlaced(environment))
                    nonExludedZoneEnvironments.Add(zoneEnvironment);
            }
            return nonExludedZoneEnvironments;
        }

        public bool GetPlaced(Environment environment)
        {
            if (!hasBeenPlaced.TryGetValue(environment, out bool placed))
            {
                placed = false;
                hasBeenPlaced[environment] = placed;
            }
            return placed;
        }

        public void SetPlaced(ZoneEnvironment zoneEnvironment)
        {
            Environment environment = zoneEnvironment.environment;
            hasBeenPlaced[environment] = true;
        }
    }
}
