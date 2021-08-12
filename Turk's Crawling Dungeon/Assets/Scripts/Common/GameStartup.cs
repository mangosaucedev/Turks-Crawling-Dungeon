using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.IO;

namespace TCD
{
    public class GameStartup
    {
        public bool isFinished;

        public IEnumerator StartGame()
        {
            PrefabLoader prefabLoader = new PrefabLoader();
            yield return prefabLoader.LoadAllAtPath();

            TileLoader tileLoader = new TileLoader();
            yield return tileLoader.LoadAllAtPath();

            AudioClipLoader audioClipLoader = new AudioClipLoader();
            yield return audioClipLoader.LoadCoreAudioClips();

            SpriteLoader spriteLoader = new SpriteLoader();
            yield return spriteLoader.LoadCoreSprites();

            ObjectsDeserializer objectsDeserializer = new ObjectsDeserializer();
            yield return objectsDeserializer.DeserializeRawsAtPath();

            AttackDeserializer attackDeserializer = new AttackDeserializer();
            yield return attackDeserializer.DeserializeRawsAtPath();

            DamageTypeDeserializer damageTypeDeserializer = new DamageTypeDeserializer();
            yield return damageTypeDeserializer.DeserializeRawsAtPath();

            EnvironmentDeserializer environmentDeserializer = new EnvironmentDeserializer();
            yield return environmentDeserializer.DeserializeRawsAtPath();

            ZoneDeserializer zoneDeserializer = new ZoneDeserializer();
            yield return zoneDeserializer.DeserializeRawsAtPath();

            GameTextDeserializer gameTextDeserializer = new GameTextDeserializer();
            yield return gameTextDeserializer.DeserializeRawsAtPath();

            KeybindingsLoader keybindingsLoader = new KeybindingsLoader();
            keybindingsLoader.TryToLoad();

            EventManager.Send(new GameStartupFinishedEvent());
            isFinished = true;
        }
    }
}
