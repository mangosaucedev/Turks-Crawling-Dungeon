using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface ILoadingOperation 
    {
        public string Name { get; }

        public string Message { get; }

        public string Description { get; }

        public bool HasStarted { get; set; }

        public bool IsDone { get; set; }

        public float Progress { get; }

        public IEnumerator Load();
    }
}
