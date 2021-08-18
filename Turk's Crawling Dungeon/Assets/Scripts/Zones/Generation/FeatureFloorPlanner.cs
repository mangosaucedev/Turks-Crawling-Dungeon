using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class FeatureFloorPlanner : GeneratorObject
    {
        private TGrid<ChamberCellType> Cells => Zone.CellTypes;

        public override IEnumerator Generate()
        {
            yield return BuildChambers();
            yield return BuildCorridors();
        }

        private IEnumerator BuildCorridors()
        {
            foreach (ICorridor corridor in Zone.Corridors)
            {
                BuildCorridor(corridor);
                yield return null;
            }
        }

        private void BuildCorridor(ICorridor corridor)
        {
            foreach (Vector2Int position in corridor.Cells)
                PlanFloor(position.x, position.y);
        }

        private void PlanFloor(int x, int y) =>
            Cells[x, y] = ChamberCellType.Floor;

        private IEnumerator BuildChambers()
        {
            foreach (IChamber chamber in Zone.Chambers)
            {
                BuildChamber(chamber);
                yield return null;
            }
        }

        private void BuildChamber(IChamber chamber)
        {
            int xMin = chamber.X;
            int yMin = chamber.Y;
            int width = chamber.Width;
            int height = chamber.Height;
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    int xReal = xMin + x;
                    int yReal = yMin + y;
                    if (chamber.Cells[x, y] != ChamberCellType.None)
                        PlanFloor(xReal, yReal);
                }
        }
    }
}
