using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones
{
    public class ZonePopulator : GeneratorObject
    {
        private IChamber currentChamber;
       
        public override IEnumerator Generate()
        {
            int i = 0;
            foreach (IChamber chamber in Zone.Chambers)
            {
                if (i < 3)
                {
                    i++;
                    continue;
                }
                int random = RandomInfo.Random.Next(0, 3);
                if (random > 0)
                {
                    currentChamber = chamber;
                    PopulateFeatureWithEnemies();
                }
                yield return null;
            }
        }

        private void PopulateFeatureWithEnemies()
        {
            int enemyCount = RandomInfo.Random.Next(1, 5);
            for (int i = 0; i < enemyCount; i++)
                PlaceEnemy();
        }

        private void PlaceEnemy()
        {
            if (TryFindEmptyPositionInFeature(out Vector2Int position))
            {
                string obj = Choose.Random(new string[] { "GenericEnemy", "Tarantula", "Shrunk", "Shrunk", "WhiteHand" });
                ObjectFactory.BuildFromBlueprint(obj, position);
            }
        }

        private bool TryFindEmptyPositionInFeature(out Vector2Int position)
        {
            position = Vector2Int.zero;
            List<Vector2Int> emptyPositions = new List<Vector2Int>();
            int xMin = currentChamber.BoundsInt.xMin;
            int yMin = currentChamber.BoundsInt.yMin;
            int xMax = currentChamber.BoundsInt.xMax;
            int yMax = currentChamber.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (PositionChecker.IsEmpty(x, y) && PositionChecker.IsFloored(x, y))
                        emptyPositions.Add(new Vector2Int(x, y));
                }
            if (emptyPositions.Count == 0)
                return false;
            position = Choose.Random(emptyPositions);
            return true;
        }
    }
}
