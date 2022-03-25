using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ZoneUnloadOperation : ILoadingOperation
    {
        private IEnumerator routine;

        public string Name => "Zone Unload Operation";

        public string Message => "Unloading zone...";

        public string Description => Message;

        public bool HasStarted { get; set; }

        public bool IsDone { get; set; }

        public float Progress => 0f;

        public ZoneUnloadOperation(IEnumerator routine)
        {
            this.routine = routine;
        }

        public IEnumerator Load()
        {
            yield return routine;
            IsDone = true;
        }
    }
}
