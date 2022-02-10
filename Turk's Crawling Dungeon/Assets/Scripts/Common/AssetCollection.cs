using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AssetCollection
    {
        public Dictionary<string, object> collection =
            new Dictionary<string, object>();

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

        public void Add(string name, object obj) =>
            collection[name] = obj;
    }
}