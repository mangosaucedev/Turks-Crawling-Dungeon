using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones
{
    public class ZoneLootPlanner : GeneratorObject
    {
        private IFeature currentFeature;

        public override IEnumerator Generate()
        {
            foreach (IFeature feature in Zone.Features)
            {
                currentFeature = feature;
                PlaceCoins();
                yield return null;
            }
        }

        private void PlaceCoins()
        {
            if (TryGetPointWithinFeature(out Vector2Int position))
            {
                string obj = Choose.Random(new string[] { "LooseChange", "LooseBills", "Spinel", "Zircon", "SharpStone", "SharpStone", "HealthPotion"});
                ObjectFactory.BuildFromBlueprint(obj, position);
            }
        }

        private bool TryGetPointWithinFeature(out Vector2Int position)
        {
            position = Vector2Int.zero;
            List<Vector2Int> points = new List<Vector2Int>();
            int xMin = currentFeature.BoundsInt.xMin;
            int xMax = currentFeature.BoundsInt.xMax;
            int yMin = currentFeature.BoundsInt.yMin;
            int yMax = currentFeature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (PositionChecker.IsEmpty(x, y) && PositionChecker.IsFloored(x, y))
                        points.Add(new Vector2Int(x, y));
                }
            if (points.Count == 0)
                return false;
            position = Choose.Random(points);
            return true;
        }
    }
}
