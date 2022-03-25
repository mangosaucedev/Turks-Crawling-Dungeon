using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD
{
    public class ZoneGenerationOperation : ILoadingOperation
    {
        private ZoneGenerator generator;

        public string Name => "Zone Generation Operation";

        public string Message => generator.currentMachine?.LoadMessage ?? "Generation zone...";

        public string Description => Message;

        public bool HasStarted { get; set; }

        public bool IsDone { get; set; }

        public float Progress => generator.Progress;

        public ZoneGenerationOperation(ZoneGenerator generator)
        {
            this.generator = generator;
        }

        public IEnumerator Load()
        {
            yield return generator.GenerateZoneRoutine();
            IsDone = true;
        }
    }
}
