using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO
{
    public class ResourceLoader<T> where T : Object
    {
        private int resourcesToLoad;
        private int resourcesLoaded;

        public float Progress
        {
            get
            {
                if (resourcesToLoad == 0)
                    return 0f;
                return (float) resourcesLoaded / resourcesToLoad;
            }
        }

        public IEnumerator LoadAll()
        {
            T[] resources = Resources.LoadAll<T>("");
            resourcesToLoad = resources.Length;
            foreach (T resource in resources)
            {
                Assets.Add(resource.name, resource);
                resourcesLoaded++;
                yield return null;
            }
            DebugLogger.Log("Finished importing " + resourcesLoaded + " " + typeof(T).Name + " resources.");
        }
    }
}
