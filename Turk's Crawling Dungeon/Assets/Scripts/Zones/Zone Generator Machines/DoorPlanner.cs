using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class DoorPlanner : ZoneGeneratorMachine
    {
        private const int NEIGHBORING_WALLS_FOR_DOOR = 4;

        private TGrid<ChamberCellType> Cells => Zone.CellTypes;

        public override IEnumerator Generate()
        {
            foreach (ICorridor corridor in Zone.Corridors)
            {
                PlaceDoorsForCorridor(corridor);
                yield return null;
            }
        }

        private void PlaceDoorsForCorridor(ICorridor corridor)
        {
            foreach (Vector2Int position in corridor.Cells)
                EvaluateCellForDoorPlacement(position.x, position.y);
        }

        private void EvaluateCellForDoorPlacement(int x, int y)
        {
            if (GetNeighboringWalls(x, y) == NEIGHBORING_WALLS_FOR_DOOR && IsDoorway(x, y))
                Cells[x, y] = ChamberCellType.Door;
        }
        
        private int GetNeighboringWalls(int x, int y)
        {
            int count = 0;
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int checkX = x + xOffset;
                    int checkY = y + yOffset;
                    if ((checkX == x && checkY == y) || !Cells.IsWithinBounds(checkX, checkY))
                        continue;
                    if (Cells[checkX, checkY] == ChamberCellType.Wall)
                        count++;
                }
            return count;
        }

        private bool IsDoorway(int x, int y)
        {
            bool wallNorth = CheckFor(x, y + 1, ChamberCellType.Wall);
            bool wallSouth = CheckFor(x, y - 1, ChamberCellType.Wall);
            bool wallWest = CheckFor(x - 1, y, ChamberCellType.Wall);
            bool wallEast = CheckFor(x + 1, y, ChamberCellType.Wall);
            return ((wallNorth && wallSouth && !wallWest && !wallEast) || 
                    (wallWest && wallEast && !wallSouth && !wallNorth));
        }

        private bool CheckFor(int x, int y, ChamberCellType type)
        {
            if (!Cells.IsWithinBounds(x, y))
                return false;
            return Cells[x, y] == type;
        }
    }
}
