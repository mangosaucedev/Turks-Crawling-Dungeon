using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones
{
    public class ZoneLootPlanner : ZoneGeneratorMachine
    {
        private IFeature currentFeature;

        public override string LoadMessage => "Planning loot...";

        public override IEnumerator Generate()
        {
            foreach (IFeature feature in Zone.Features)
            {
                currentFeature = feature;
                TestPlace();
                yield return null;
            }
        }

        private void TestPlace()
        {
            if (TryGetPointWithinFeature(out Vector2Int position))
            {
                string[] money = new string[] { "LooseChange", "LooseBills", "Spinel", "Zircon" };
                string[] weapons = new string[] { "RustyDagger", "SharpStone", "Branch", "ThrowingRock" };
                string[] wearables = new string[] { "NeckRuff", "PierrotBlouse", "PierrotPants", "GoldBand" };
                string[] health = new string[] { "HealthPotion" };
                string[] chosenArray = Choose.Random(money, weapons, wearables, health);
                string obj = Choose.Random(chosenArray);
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
