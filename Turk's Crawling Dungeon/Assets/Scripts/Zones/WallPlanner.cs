using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class WallPlanner : GeneratorObject
    {
        private TGrid<ChamberCellType> Cells => Zone.CellTypes;

        public override IEnumerator Generate()
        {
            yield return PlaceWallsAroundChambers();
            yield return PlaceWallsAroundCorridors();
        }

        private IEnumerator PlaceWallsAroundChambers()
        {
            foreach (IChamber chamber in Zone.Chambers)
            {
                PlaceWallAroundChamber(chamber);
                yield return null;
            }
        }

        private void PlaceWallAroundChamber(IChamber chamber)
        {
            int xMin = chamber.X;
            int yMin = chamber.Y;
            int xMax = xMin + chamber.Width;
            int yMax = yMin + chamber.Height;
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

        private IEnumerator PlaceWallsAroundCorridors()
        {
            foreach (ICorridor corridor in Zone.Corridors)
            {
                PlaceWallAroundCorridor(corridor);
                yield return null;
            }
        }

        private void PlaceWallAroundCorridor(ICorridor corridor)
        {
            foreach (Vector2Int position in corridor.Cells)
                EvaluateSurroundingCellsToPlaceWall(position.x, position.y);
        }
    }
}
