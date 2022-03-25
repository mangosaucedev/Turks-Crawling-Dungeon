using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public abstract class ZoneGenerator 
    {
        public IZone zone;
        public ZoneGeneratorMachine currentMachine;
        public Queue<ZoneGeneratorMachine> machines = new Queue<ZoneGeneratorMachine>();

        private int machineIndex;

        public float Progress
        {
            get
            {
                if (machines.Count == 0)
                    return 0f;
                return (float) machineIndex / machines.Count;
            }
        }

        public IEnumerator GenerateZoneRoutine()
        {
            foreach(ZoneGeneratorMachine machine in machines)
            {
                currentMachine = machine;
                yield return machine.Generate();
                machineIndex++;
            }
        }
    }
}
