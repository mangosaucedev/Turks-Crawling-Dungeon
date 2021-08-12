using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class EnvironmentFurnisher : GeneratorObject
    {
        private IFeature currentFeature;
        private Environment currentEnvironment;

        public override IEnumerator Generate()
        {
            foreach (IFeature feature in Zone.Features)
            {
                currentFeature = feature;
                currentEnvironment = feature.Environment;
                DeployForcedFurnishersOnFeature();
                FurnishFeatureRandomly();
                yield return null;
            } 
        }

        private void DeployForcedFurnishersOnFeature()
        {
            foreach (Furnisher furnisher in currentEnvironment.GetForcedFurnishers())
                FurnishFeature(furnisher);
        }

        private void FurnishFeature(Furnisher furnisher)
        {
            if (TryGetPointWithinFeature(out Vector2Int position))
            {
                furnisher.Furnish(currentFeature, position.x, position.y);
                currentEnvironment.FlagFurnisherAsUsed(furnisher);
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
                    if (CanFurnish(x, y))
                        points.Add(new Vector2Int(x, y));
                }
            if (points.Count == 0)
                return false;
            position = Choose.Random(points);
            return true;
        }

        private int GetPasses() =>
            Mathf.CeilToInt(currentFeature.BoundsInt.size.magnitude * currentFeature.Environment.furnishingDensity);
        

        private bool CanFurnish(int x, int y) => Zone.Environments.IsWithinBounds(x, y) && 
            Zone.Environments[x, y] != null &&
            PositionChecker.IsEmpty(x, y);

        private void FurnishFeatureRandomly()
        {
            for (int i = 0; i <= GetPasses(); i++)
                FurnishAtRandom();
        }

        private void FurnishAtRandom()
        {
            using (GrabBag<Furnisher> bag = new GrabBag<Furnisher>())
            {
                foreach (Furnisher furnisher in currentEnvironment.GetNonExcludedFurnishers())
                    bag.AddItem(furnisher, furnisher.weight);
                Furnisher chosenFurnisher = bag.Grab();
                FurnishFeature(chosenFurnisher);
            }
        }
    }
}
