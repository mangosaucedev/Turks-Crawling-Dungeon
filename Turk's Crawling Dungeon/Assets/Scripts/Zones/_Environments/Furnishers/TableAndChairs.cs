using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones.Environments
{
    public class TableAndChairs : Furnisher
    {
        private const short MAX_TRIES = 16;

        private List<GameObject> spawnedObjects = new List<GameObject>();

        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            for (int i = 0; i < MAX_TRIES; i++)
            {
                if (!TryMakingTableAndChairs(x, y))
                    DestroyAllSpawnedObjects();
                else
                    break;
            }
        }

        private bool TryMakingTableAndChairs(int x, int y)
        {
            spawnedObjects.Clear();
            if (PositionChecker.IsEmptyInRadius(x, y, 1, 1))
            {
                string table = environment.GetRandomFurnishing("Table");
                string chair = environment.GetRandomFurnishing("Chair");
                int random = RandomInfo.Random.Next(0, 2);
                if (random < 1)
                {
                    TryMakingObject(table, x, y);
                    TryMakingObject(chair, x - 1, y);
                    TryMakingObject(chair, x + 1, y);
                }
                else
                {
                    TryMakingObject(table, x, y);
                    TryMakingObject(chair, x, y - 1);
                    TryMakingObject(chair, x, y + 1);
                }
            }
            return PlayerPathChecker.ValidPathToEndRoom();
        }

        private void TryMakingObject(string obj, int x, int y)
        {
            if (PositionChecker.IsEmpty(x, y))
            {
                BaseObject spawned = ObjectFactory.BuildFromBlueprint(obj, new Vector2Int(x, y));
                GameObject gameObject = spawned.gameObject;
                spawnedObjects.Add(gameObject);
            }
        }

        private void DestroyAllSpawnedObjects()
        {
            foreach (GameObject obj in spawnedObjects)
                Object.Destroy(obj);
        }
    }
}
