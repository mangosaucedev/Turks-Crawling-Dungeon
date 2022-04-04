using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AssetCollection
    {
        private Dictionary<string, object> collection =
            new Dictionary<string, object>();
        private HashSet<string> assetNames = new HashSet<string>();

        public T Get<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                ExceptionHandler.Handle(new Exception($"Cannot retrieve asset with no name!"));
            if (collection.TryGetValue(name, out object obj))
                return (T) obj;
            ExceptionHandler.Handle(new Exception($"No {typeof(T).Name} asset exists with name {name}!"));
            return default;
        }

        public bool Exists(string name) => collection.ContainsKey(name);

        public void Add(string name, object obj)
        {
            assetNames.Add(name);
            collection[name] = obj;
        }

        public List<T> FindAll<T>(string nameContains)
        {
            List<T> assets = new List<T>();
            foreach (string name in assetNames)
            {
                if (name.Contains(nameContains))
                {
                    T asset = (T)collection[name];
                    assets.Add(asset);
                }
            }
            return assets;
        }
    }
}