using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class WallPlanner : ZoneGeneratorMachine
    {
        public override IEnumerator Generate()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if (Zone.CellTypes[x, y] == ChamberCellType.Floor)
                        EvaluateIsFloorWall(x, y);
                }
            yield return null;
        }

        private void EvaluateIsFloorWall(int x, int y)
        {
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int dx = x + xOffset;
                    int dy = y + yOffset;
                    if (dx == x && dy == y)
                        continue;
                    if (!Zone.CellTypes.IsWithinBounds(dx, dy) || Zone.CellTypes[dx, dy] < ChamberCellType.Floor)
                    {
                        Zone.CellTypes[x, y] = ChamberCellType.Wall;
                        return;
                    }    
                }
        }
    }
}
