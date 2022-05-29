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

            currentZone = new Zone(zoneParams);

            if (!string.IsNullOrEmpty(blueprint.zoneEnvironmentsName))
            {
                ZoneEnvironments zoneEnvironments = Assets.Get<ZoneEnvironments>(blueprint.zoneEnvironmentsName);
                if (zoneEnvironments.environments.Count == 0)
                    SetupZoneEnvironments(zoneEnvironments);
                currentZone.ZoneEnvironments = zoneEnvironments;
            }

            ZoneTerrain zoneTerrain = Assets.Get<ZoneTerrain>(blueprintName);
            currentZone.ZoneTerrain = zoneTerrain;


            if (!string.IsNullOrEmpty(blueprint.zoneEncountersName))
            {
                ZoneEncounters zoneEncounters = (ZoneEncounters)Assets.Get<ZoneEncounters>(blueprint.zoneEncountersName).Clone();
                zoneEncounters.density = blueprint.encounterDensity;
                zoneEncounters.BuildEncounters();
                currentZone.ZoneEncounters = zoneEncounters;
            }

            currentZone.cinematicName = blueprint.cinematicName;
            currentZone.CustomGeneratorMachineNames = blueprint.CustomGeneratorMachineNames;

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
