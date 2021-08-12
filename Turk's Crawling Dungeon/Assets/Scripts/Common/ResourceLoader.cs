using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TCD
{
    public abstract class ResourceLoader<T> where T : Object
    {
        public abstract string ResourcePath { get; }

        public IEnumerator LoadAllAtPath()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            T[] resources = Resources.LoadAll<T>(ResourcePath);
            foreach (T resource in resources)
            {
                Assets.Add(resource.name, resource);
                yield return null;
            }
            stopwatch.Stop();
            DebugLogger.Log(
                $"Loaded {resources.Length} {typeof(T).Name} resources " +
                $"from path /{ResourcePath}/ in {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
