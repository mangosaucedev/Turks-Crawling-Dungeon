using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class CavernPlanner : ZoneGeneratorMachine
    {
        private const int BORDER_SIZE = 16;

        public override IEnumerator Generate()
        {
            int size = Width * Height / 5;
            Cavern cavern = CavernFactory.BuildCavern(BORDER_SIZE, BORDER_SIZE, Width - BORDER_SIZE * 2, Height - BORDER_SIZE * 2, size);
            foreach (Vector2Int position in cavern.OccupiedPositions)
                Zone.CellTypes[position + new Vector2Int(BORDER_SIZE, BORDER_SIZE)] = ChamberCellType.Floor;
            Zone.Features.Add(cavern);
            yield break;
        }
    }
}
