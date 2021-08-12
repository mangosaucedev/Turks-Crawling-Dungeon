using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public interface IZone
    {
        IZoneParams ZoneParams { get; set; }

        ZoneEnvironments ZoneEnvironments { get; set; }

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
