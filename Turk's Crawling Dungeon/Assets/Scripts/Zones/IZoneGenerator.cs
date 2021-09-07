using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public interface IZoneGenerator
    {
        IZoneParams ZoneParams { get; }

        IEnumerator GenerateZoneRoutine(ZoneGeneratorType type);
    }
}
