using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public static class ZoneFactory 
    {
        private static Zone currentZone;

        public static IZone BuildFromBlueprint(string blueprintName)
        {
            Zone blueprint = Assets.Get<Zone>(blueprintName);
            IZoneParams zoneParams = Assets.Get<IZoneParams>(blueprint.zoneParamsName);
            ZoneEnvironments zoneEnvironments = Assets.Get<ZoneEnvironments>(blueprint.zoneEnvironmentsName);
            if (zoneEnvironments.environments.Count == 0)
                SetupZoneEnvironments(zoneEnvironments);
            ZoneTerrain zoneTerrain = Assets.Get<ZoneTerrain>(blueprintName);
            ZoneEncounters zoneEncounters = (ZoneEncounters) Assets.Get<ZoneEncounters>(blueprint.zoneEncountersName).Clone();
            zoneEncounters.density = blueprint.encounterDensity;
            zoneEncounters.BuildEncounters();
            currentZone = new Zone(zoneParams);
            currentZone.cinematicName = blueprint.cinematicName;
            currentZone.ZoneEnvironments = zoneEnvironments;
            currentZone.ZoneTerrain = zoneTerrain;
            currentZone.ZoneEncounters = zoneEncounters;
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
