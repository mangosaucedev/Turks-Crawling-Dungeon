using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public interface IZoneParams
    {
        int Width { get; set; }
        
        int Height { get; set; }

        int MinChamberWidth { get; set; }
        
        int MinChamberHeight { get; set; }

        int MaxChamberWidth { get; set; }
        
        int MaxChamberHeight { get; set; }

        int MaxChambers { get; set; }

        int MinChamberChildDistance { get; set; }

        int MinChamberBufferDistance { get; set; }

        int MinCorridorWidth { get; set; }

        int MaxCorridorWidth { get; set; }

        bool GenerateCaves { get; set; }

        int CavePenetrationChance { get; set; }

    }
}
