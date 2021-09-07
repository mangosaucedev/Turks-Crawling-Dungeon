using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class WallPlanner : ZoneGeneratorMachine
    {
        private TGrid<ChamberCellType> Cells => Zone.CellTypes;

        public override IEnumerator Generate()
        {
            foreach (IFeature feature in Zone.Features)
            {
                PlaceWallAroundFeature(feature);
                yield return null;
            }
        }

        private void PlaceWallAroundFeature(IFeature feature)
        {
            int xMin = feature.BoundsInt.xMin;
            int yMin = feature.BoundsInt.yMin;
            int xMax = feature.BoundsInt.xMax;
            int yMax = feature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (Cells[x, y] == ChamberCellType.Floor)
                        EvaluateSurroundingCellsToPlaceWall(x, y);
                }
        }

        private void EvaluateSurroundingCellsToPlaceWall(int xOrigin, int yOrigin)
        {
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int x = xOrigin + xOffset;
                    int y = yOrigin + yOffset;
                    if ((x == xOrigin && y == yOrigin) || !Cells.IsWithinBounds(x, y)) 
                        continue;
                    if (Cells[x, y] == ChamberCellType.None)
                        PlaceWall(x, y);
                }
        }

        private void PlaceWall(int x, int y) =>
            Cells[x, y] = ChamberCellType.Wall;
    }
}
