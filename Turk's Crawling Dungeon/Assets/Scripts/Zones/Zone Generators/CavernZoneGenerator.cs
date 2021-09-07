using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class CavernZoneGenerator  : ZoneGenerator
    {
        public override IEnumerator RunGenerationMachinesRoutine()
        {
            CavernPlanner cavernPlanner = new CavernPlanner();
            yield return cavernPlanner.Generate();

            FeatureFloorPlanner featureFloorPlanner = new FeatureFloorPlanner();
            yield return featureFloorPlanner.Generate();

            WallPlanner wallPlanner = new WallPlanner();
            yield return wallPlanner.Generate();

            DoorPlanner doorPlanner = new DoorPlanner();
            yield return doorPlanner.Generate();

            ZoneBuilder zoneBuilder = new ZoneBuilder();
            yield return zoneBuilder.Generate();

            ZoneColorer zoneColorer = new ZoneColorer();
            yield return zoneColorer.Generate();

            EnvironmentPlanner environmentPlanner = new EnvironmentPlanner();
            yield return environmentPlanner.Generate();

            PlayerPlacer playerPlacer = new PlayerPlacer();
            yield return playerPlacer.Generate();

            StairsPlacer stairsPlacer = new StairsPlacer();
            yield return stairsPlacer.Generate();

            EnvironmentFurnisher environmentFurnisher = new EnvironmentFurnisher();
            yield return environmentFurnisher.Generate();

            ZonePopulator zonePopulator = new ZonePopulator();
            yield return zonePopulator.Generate();

            ZoneLootPlanner zoneLootPlanner = new ZoneLootPlanner();
            yield return zoneLootPlanner.Generate();
        }
    }
}
