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

            GameTextDeserializer gameTextDeserializer = new GameTextDeserializer();
            yield return gameTextDeserializer.DeserializeRawsAtPath();

            KeybindingsLoader keybindingsLoader = new KeybindingsLoader();
            keybindingsLoader.TryToLoad();

            EventManager.Send(new GameStartupFinishedEvent());
            isFinished = true;
        }
    }
}
