using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public interface IZone
    {
        Cinematic Cinematic { get; }

        IZoneParams ZoneParams { get; set; }

        ZoneEnvironments ZoneEnvironments { get; set; }

        ZoneTerrain ZoneTerrain { get; set; }

        ZoneEncounters ZoneEncounters { get; set; }

        int Width { get; }

        int Height { get; }

        List<IFeature> Features { get; }

        List<IChamber> Chambers { get; }

        List<ICorridor> Corridors { get; }

        TGrid<ChamberCellType> CellTypes { get; }

        TGrid<Environment> Environments { get; }

        List<IFeature> GetUndesignatedFeatures();
    }
}
