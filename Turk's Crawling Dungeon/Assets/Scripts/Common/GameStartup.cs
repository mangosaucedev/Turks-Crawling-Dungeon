using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TCD.IO;

namespace TCD
{
    public static class GameStartup
    {
        public static bool isFinished;

        private static Type[] assemblyTypes;

        public static IEnumerator StartGame()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GameStartup));
            assemblyTypes = assembly.GetTypes();

            AudioClipLoader audioClipLoader = new AudioClipLoader();
            yield return audioClipLoader.LoadCoreAudioClips();

            TextureLoader textureLoader = new TextureLoader();
            yield return textureLoader.LoadCoreSprites();

            KeybindingsLoader keybindingsLoader = new KeybindingsLoader();
            keybindingsLoader.TryToLoad();

            yield return LoadAssets();

            EventManager.Send(new GameStartupFinishedEvent());
            isFinished = true;
            DebugLogger.Log("Game startup complete. All assets loaded!");
        }

        private static IEnumerator LoadAssets()
        {
            DebugLogger.Log("Loading assets...");
            LoadingManager loadingManager = ServiceLocator.Get<LoadingManager>();
            foreach (Type type in assemblyTypes)
            {
                AssetLoaderAttribute attribute = type.GetCustomAttribute<AssetLoaderAttribute>();
                if (attribute != null)
                {
                    IAssetLoader loader = (IAssetLoader) Activator.CreateInstance(type);
                    yield return loadingManager.EnqueueLoadingOperationRoutine(new AssetLoadingOperation(loader));
                }
            }
        }
    }
}
