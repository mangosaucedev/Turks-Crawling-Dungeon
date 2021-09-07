using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Zones.Utilities;

namespace TCD.Zones.Environments
{
    public class Lake : Furnisher
    {
        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            Vector2Int position = new Vector2Int(x, y);
            string liquid = environment.GetRandomFurnishing("Lake");
            if (string.IsNullOrEmpty(liquid))
                return;
            int size = RandomInfo.Random.Next(6, 14);
            for (int i = 0; i <= size; i++)
            {
                if (!PositionChecker.IsObstacle(position) && PositionChecker.IsFloored(position))
                    ObjectFactory.BuildFromBlueprint(liquid, position);
                int xOffset = RandomInfo.Random.Next(-1, 1);
                int yOffset = RandomInfo.Random.Next(-1, 1);
                Vector2Int offset = new Vector2Int(xOffset, yOffset);
                if ((xOffset == 0 && yOffset == 0) || 
                    !CurrentZoneInfo.grid.IsWithinBounds(position + offset))
                    continue;
                position += offset;
            }
        }
    }
}
