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

            PrefabLoader prefabLoader = new PrefabLoader();
            yield return prefabLoader.LoadAllAtPath();

            MaterialsLoader materialsLoader = new MaterialsLoader();
            yield return materialsLoader.LoadAll();

            TileLoader tileLoader = new TileLoader();
            yield return tileLoader.LoadAllAtPath();

            AudioClipLoader audioClipLoader = new AudioClipLoader();
            yield return audioClipLoader.LoadCoreAudioClips();

            TextureLoader spriteLoader = new TextureLoader();
            yield return spriteLoader.LoadCoreSprites();

            ColorDeserializer colorDeserializer = new ColorDeserializer();
            yield return colorDeserializer.DeserializeRawsAtPath();

            ObjectsDeserializer objectsDeserializer = new ObjectsDeserializer();
            yield return objectsDeserializer.DeserializeRawsAtPath();
            
            EncountersDeserializer encountersDeserializer = new EncountersDeserializer();
            yield return encountersDeserializer.DeserializeRawsAtPath();

            AttackDeserializer attackDeserializer = new AttackDeserializer();
            yield return attackDeserializer.DeserializeRawsAtPath();

            DamageTypeDeserializer damageTypeDeserializer = new DamageTypeDeserializer();
            yield return damageTypeDeserializer.DeserializeRawsAtPath();

            EnvironmentDeserializer environmentDeserializer = new EnvironmentDeserializer();
            yield return environmentDeserializer.DeserializeRawsAtPath();

            ZoneEncountersDeserializer zoneEncountersDeserializer = new ZoneEncountersDeserializer();
            yield return zoneEncountersDeserializer.DeserializeRawsAtPath();

            ZoneEnvironmentsDeserializer zoneEnvironmentsDeserializer = new ZoneEnvironmentsDeserializer();
            yield return zoneEnvironmentsDeserializer.DeserializeRawsAtPath();

            ZoneParamsDeserializer zoneParamsDeserializer = new ZoneParamsDeserializer();
            yield return zoneParamsDeserializer.DeserializeRawsAtPath();

            ZoneDeserializer zoneDeserializer = new ZoneDeserializer();
            yield return zoneDeserializer.DeserializeRawsAtPath();

            DungeonDeserializer dungeonDeserializer = new DungeonDeserializer();
            yield return dungeonDeserializer.DeserializeRawsAtPath();

            CampaignDeserializer campaignDeserializer = new CampaignDeserializer();
            yield return campaignDeserializer.DeserializeRawsAtPath();

            TileDeserializer tileDeserializer = new TileDeserializer();
            yield return tileDeserializer.DeserializeRawsAtPath();

            GameTextDeserializer gameTextDeserializer = new GameTextDeserializer();
            yield return gameTextDeserializer.DeserializeRawsAtPath();

            DialogueNodeDeserializer dialogueNodeDeserializer = new DialogueNodeDeserializer();
            yield return dialogueNodeDeserializer.DeserializeRawsAtPath();

            DialogueSpeakerDeserializer dialogueSpeakerDeserializer = new DialogueSpeakerDeserializer();
            yield return dialogueSpeakerDeserializer.DeserializeRawsAtPath();

            KeybindingsLoader keybindingsLoader = new KeybindingsLoader();
            keybindingsLoader.TryToLoad();

            yield return DeserializeAssets();

            EventManager.Send(new GameStartupFinishedEvent());
            isFinished = true;
        }

        private static IEnumerator LoadXmlAssets()
        {
            yield return null;
        }

        private static IEnumerator DeserializeAssets()
        {
            DebugLogger.Log("Deserializing assets...");
            LoadingManager loadingManager = ServiceLocator.Get<LoadingManager>();
            foreach (Type type in assemblyTypes)
            {
                AssetDeserializerAttribute attribute = type.GetCustomAttribute<AssetDeserializerAttribute>();
                if (attribute != null)
                {
                    IDeserializer deserializer = (IDeserializer) Activator.CreateInstance(type);
                    yield return loadingManager.EnqueueLoadingOperationRoutine(new AssetLoadingOperation(deserializer));
                }
            }
        }
    }
}
