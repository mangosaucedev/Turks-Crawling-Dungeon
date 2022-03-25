using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class GenericZoneGenerator : ZoneGenerator
    {
        public GenericZoneGenerator()
        {
            machines.Enqueue(new ChamberPlanner());
            machines.Enqueue(new CorridorPlanner());
            machines.Enqueue(new FeatureFloorPlanner());
            machines.Enqueue(new WallPlanner());
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
