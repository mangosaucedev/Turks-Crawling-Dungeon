using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TCD.IO
{
    public abstract class JsonDeserializer<T> : IDeserializer
    {
        protected int deserializedFiles;

        protected abstract string FullPath { get; }
        
        protected abstract string Extension { get; }

        public virtual IEnumerator DeserializeAll()
        {
            var paths = from path in Directory.GetFiles(FullPath, $"*.{Extension}")
                        where !path.EndsWith(".meta")
                        select path;
            foreach (string path in paths)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(path);
                    string json = File.ReadAllText(path);
                    T obj = JsonUtility.FromJson<T>(json);
                    AddAsset(name, obj);
                    deserializedFiles++;
                }
                catch (Exception e)
                {
                    string fileName = Path.GetFileName(path);
                    ExceptionHandler.HandleMessage(
                        "Could not deserialize " + typeof(T).Name + " from " + fileName + ": " + e.Message);
                }
                yield return null;
            }

            DebugLogger.Log("Successfully deserialized " + deserializedFiles + " " + typeof(T).Name + "(s).");
        }

        public virtual void AddAsset(string name, T obj)
        {
            Assets.Add(name, obj);
        }
    }
}
