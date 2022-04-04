using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace TCD.IO
{
    public abstract class JsonDeserializer<T> : IAssetLoader
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings 
        { 
            NullValueHandling = NullValueHandling.Ignore,
            StringEscapeHandling = StringEscapeHandling.EscapeHtml 
        };

        protected int filesToDeserialize;
        protected int deserializedFiles;

        public float Progress
        {
            get
            {
                if (filesToDeserialize == 0)
                    return 1f;
                return (float) deserializedFiles / filesToDeserialize;
            }
        }

        protected abstract string FullPath { get; }
        
        protected abstract string Extension { get; }

        public virtual IEnumerator LoadAll()
        {
            List<string> paths = (from path in Directory.GetFiles(FullPath, $"*.{Extension}")
                        where !path.EndsWith(".meta")
                        select path).ToList();
            filesToDeserialize = paths.Count;
            foreach (string path in paths)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(path);
                    string json = File.ReadAllText(path);
                    T obj = JsonConvert.DeserializeObject<T>(json, settings);
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
