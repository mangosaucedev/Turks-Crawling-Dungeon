using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class CorridorCellCollection
    {
        private Dictionary<Vector2Int, ChamberCellType> cells = 
            new Dictionary<Vector2Int, ChamberCellType>();
        private HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        public void Add(Vector2Int position, ChamberCellType cell)
        {
            if (!positions.Contains(position))
            {
                positions.Add(position);
                cells[position] = cell;
            }
        }

        public HashSet<Vector2Int> GetPositions() => positions;
    
        public ChamberCellType Get(Vector2Int position) => cells[position];
    }
}
