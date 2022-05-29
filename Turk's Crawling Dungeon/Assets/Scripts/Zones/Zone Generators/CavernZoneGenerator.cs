using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class CavernZoneGenerator  : ZoneGenerator
    {
        public CavernZoneGenerator()
        {
            machines.Enqueue(new CavernPlanner());
            machines.Enqueue(new FeatureFloorPlanner());
            machines.Enqueue(new FeatureWallPlanner());
            machines.Enqueue(new DoorPlanner());
            machines.Enqueue(new ZoneBuilder());
            machines.Enqueue(new ZoneColorer());
            machines.Enqueue(new EnvironmentPlanner());
            machines.Enqueue(new PlayerPlacer());
            machines.Enqueue(new StairsPlacer());
            machines.Enqueue(new EnvironmentFurnisher());
            machines.Enqueue(new ZonePopulator());
            machines.Enqueue(new ZoneLootPlanner());
        }
    }
}
