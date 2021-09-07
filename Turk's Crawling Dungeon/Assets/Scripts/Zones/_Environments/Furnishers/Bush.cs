using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones.Environments
{
    public class Bush : Furnisher
    {
        private const short MIN_OBJECTS = 4;
        private const short MAX_OBJECTS = 9;
        private const short MAX_TRIES = 16;

        private int checkX;
        private int checkY;
        private string[] objects;
        private List<GameObject> spawnedObjects = new List<GameObject>();

        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            for (int i = 0; i < MAX_TRIES; i++)
            {
                if (!TryMakingBushes(x, y))
                    DestroyAllSpawnedObjects();
                else
                    break;
            }
        }

        private bool TryMakingBushes(int x, int y)
        {
            spawnedObjects.Clear();
            int bushCount = RandomInfo.Random.Next(MIN_OBJECTS, MAX_OBJECTS);
            checkX = x;
            checkY = y;
            objects = GetBushObjects();
            for (int i = 0; i < bushCount; i++)
                MakeBush();
            return PlayerPathChecker.ValidPathToEndRoom();
        }

        private string[] GetBushObjects() => Choose.Random(
                new string[] { environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush") },
                new string[] { environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush") },
                new string[] { environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush") },
                new string[] { environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush") },
                new string[] { environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush"), environment.GetRandomFurnishing("Bush") });
    
        private void MakeBush()
        {
            string obj = Choose.Random(objects);
            if (PositionChecker.IsEmpty(checkX, checkY))
            {
                BaseObject spawnedBush = ObjectFactory.BuildFromBlueprint(obj, new Vector2Int(checkX, checkY));
                GameObject gameObject = spawnedBush.gameObject;
                spawnedObjects.Add(gameObject);
            }
            checkX += RandomInfo.Random.Next(-1, 2);
            checkY += RandomInfo.Random.Next(-1, 2);
        }

        private void DestroyAllSpawnedObjects()
        {
            foreach (GameObject obj in spawnedObjects)
                Object.Destroy(obj);
        }
    }
}
