using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TCD
{
    public abstract class ResourceLoader<T> : IAssetLoader where T : Object
    {
        private int resourcesToDeserialize;
        private int resourcesDeserialized;

        public abstract string ResourcePath { get; }

        public float Progress
        {
            get
            {
                if (resourcesToDeserialize == 0)
                    return 1f;
                return (float) resourcesDeserialized / resourcesToDeserialize;
            }
        }

        public IEnumerator LoadAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            T[] resources = UnityEngine.Resources.LoadAll<T>(ResourcePath);
            resourcesToDeserialize = resources.Length;
            foreach (T resource in resources)
            {
                Assets.Add(resource.name, resource);
                resourcesDeserialized++;
                yield return null;
            }
            stopwatch.Stop();
            DebugLogger.Log(
                $"Loaded {resources.Length} {typeof(T).Name} resources " +
                $"from path /{ResourcePath}/ in {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
