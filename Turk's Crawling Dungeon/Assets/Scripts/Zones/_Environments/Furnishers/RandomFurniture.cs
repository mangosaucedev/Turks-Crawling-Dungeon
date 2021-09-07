using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones.Environments
{
    public class RandomFurniture : Furnisher
    {
        private const short MAX_TRIES = 16;

        private GameObject spawnedObject;

        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            for (int i = 0; i < MAX_TRIES; i++)
            {
                if (!TryMakingRandomFurniture(x, y))
                    Object.Destroy(spawnedObject);
                else
                    break;
            }
        }

        private bool TryMakingRandomFurniture(int x, int y)
        {
            if (!PositionChecker.IsEmpty(x, y))
                return true;
            string obj = environment.GetRandomFurnishing("Random");
            BaseObject spawned = ObjectFactory.BuildFromBlueprint(obj, new Vector2Int(x, y));
            spawnedObject = spawned.gameObject;
            return (PlayerPathChecker.ValidPathToEndRoom());
        }
    }
}
