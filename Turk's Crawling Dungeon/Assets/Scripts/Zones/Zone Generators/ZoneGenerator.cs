using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public abstract class ZoneGenerator 
    {
        public IZone zone;

        public IEnumerator GenerateZoneRoutine()
        {
            yield return RunGenerationMachinesRoutine();
        }

        public abstract IEnumerator RunGenerationMachinesRoutine();
    }
}
