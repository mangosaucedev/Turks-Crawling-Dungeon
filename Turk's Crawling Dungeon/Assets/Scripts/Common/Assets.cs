using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class Assets : MonoBehaviour
    {
#if UNITY_EDITOR
        [Serializable]
        public struct AssetList
        {
            public string name;
            public List<string> assets;

            public AssetList(string name)
            {
                this.name = name;
                assets = new List<string>();
            }
        }
#endif

        private static Dictionary<Type, AssetCollection> collections =
            new Dictionary<Type, AssetCollection>();

#if UNITY_EDITOR
        private static Dictionary<Type, AssetList> assetListsByType =
            new Dictionary<Type, AssetList>();
        private static List<AssetList> viewableAssets = new List<AssetList>();
        [SerializeField] private List<AssetList> assets;
#endif


#if UNITY_EDITOR
        private void Update() => assets = viewableAssets;
#endif

        public static T Get<T>(string name)
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            return collection.Get<T>(name);
        }

        public static bool Exists<T>(string name)
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            return collection.Exists(name);
        }

        public static void Add<T>(string name, T obj)
        {
            Type type = typeof(T);
            AssetCollection collection = GetAssetCollection(type);
            collection.Add(name, obj);
#if UNITY_EDITOR
            AddToViewableAssets<T>(name);
#endif
        }

        private static AssetCollection GetAssetCollection(Type type)
        {
            if (collections.TryGetValue(type, out AssetCollection collection))
                return collection;
            collection = new AssetCollection();
            collections[type] = collection;
            return collection;
        }

        public static List<T> FindAll<T>(string nameContains = "")
        {
            Type type = typeof(T);
            AssetCollection assetCollection = GetAssetCollection(type);
            return assetCollection.FindAll<T>(nameContains);
        }

#if UNITY_EDITOR
        private static void AddToViewableAssets<T>(string name)
        {
            Type type = typeof(T);
            AssetList assetList = GetAssetList(type);
            if (!assetList.assets.Contains(name))
                assetList.assets.Add(name);
        }

        private static AssetList GetAssetList(Type type)
        {
            if (!assetListsByType.TryGetValue(type, out AssetList assetList))
            {
                assetList = new AssetList(type.Name);
                viewableAssets.Add(assetList);
                assetListsByType[type] = assetList;
            }
            return assetList;
        }
#endif
    }
}