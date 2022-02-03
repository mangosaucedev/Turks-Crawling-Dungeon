using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Dungeons;

namespace TCD.Zones
{
    public interface IZoneGenerator
    {
        IZoneParams ZoneParams { get; }

        IEnumerator GenerateZoneRoutine(Dungeon dungeon, int zoneIndex);

        IEnumerator GenerateZoneRoutine();
    }
}
