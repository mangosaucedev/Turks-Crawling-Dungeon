using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.IO;
//using TCD.IO.Persistence;

namespace TCD
{
    public class UnloadSceneOperation : ILoadingOperation
    {
        private AsyncOperation unloadOperation;

        public string Name => "Unload Scene Operation";

        public string Message => "Unloading scene...";

        public string Description => Message;

        public bool HasStarted { get; set; }

        public bool IsDone { get; set; }

        public float Progress => unloadOperation.progress;

        public UnloadSceneOperation(AsyncOperation unloadOperation)
        {
            this.unloadOperation = unloadOperation;
        }

        public IEnumerator Load()
        {
            yield return unloadOperation;
            IsDone = true;
        }
    }
}
