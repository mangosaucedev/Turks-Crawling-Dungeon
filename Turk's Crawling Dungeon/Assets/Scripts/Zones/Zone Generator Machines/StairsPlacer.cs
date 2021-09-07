using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones
{
    public class StairsPlacer : ZoneGeneratorMachine
    {
        private IFeature currentFeature;

        public override IEnumerator Generate()
        {
            if (Zone.Chambers.Count > 0)
                currentFeature = Zone.Chambers[Zone.Chambers.Count - 1];
            else
                currentFeature = Zone.Features[Zone.Features.Count - 1];
            Vector2Int position = GetRandomPosition();
            ObjectFactory.BuildFromBlueprint("DownStairs", position);
            yield break;
        }

        public Vector2Int GetRandomPosition()
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            int xMin = currentFeature.BoundsInt.xMin;
            int xMax = currentFeature.BoundsInt.xMax;
            int yMin = currentFeature.BoundsInt.yMin;
            int yMax = currentFeature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    if (PositionChecker.IsEmpty(position))
                        positions.Add(position);
                }
            if (positions.Count == 0)
                throw new Exception("Could not place down stairs in zone: no valid positions for placement!");
            return Choose.Random(positions);
        }
    }
}
