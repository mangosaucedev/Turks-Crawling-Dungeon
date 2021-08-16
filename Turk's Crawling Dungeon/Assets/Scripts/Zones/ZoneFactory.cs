using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public static class ZoneFactory 
    {
        private static IZone currentZone;

        public static IZone BuildFromBlueprint(string blueprintName)
        {
            IZoneParams zoneParams = Assets.Get<IZoneParams>(blueprintName);
            ZoneEnvironments zoneEnvironments = Assets.Get<ZoneEnvironments>(blueprintName);
            if (zoneEnvironments.environments.Count == 0)
                SetupZoneEnvironments(zoneEnvironments);
            ZoneTerrain zoneTerrain = Assets.Get<ZoneTerrain>(blueprintName);
            currentZone = new Zone(zoneParams);
            currentZone.ZoneEnvironments = zoneEnvironments;
            currentZone.ZoneTerrain = zoneTerrain;
            return currentZone;
        }

        private static void SetupZoneEnvironments(ZoneEnvironments zoneEnvironments) 
        {
            for (int i = 0; i < zoneEnvironments.environmentReferences.Count; i++)
            {
                ZoneEnvironment zoneEnvironment = zoneEnvironments.environmentReferences[i];
                Environment environment = Assets.Get<Environment>(zoneEnvironment.name);
                zoneEnvironment.environment = environment;
                zoneEnvironments.environments.Add(environment);
            }
        }
    }
}
