using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class Row
    {
        public int depth;
        public float startSlope;
        public float endSlope;

        public Row(int _depth, float _startSlope, float _endSlope)
        {
            depth = _depth;
            startSlope = _startSlope;
            endSlope = _endSlope;
        }

        public List<FOVLocalCell> GetLocalCells()
        {
            List<FOVLocalCell> cells = new List<FOVLocalCell>();
            int minPosition = (depth * startSlope).RoundUpToInt();
            int maxPosition = (depth * endSlope).RoundDownToInt();
            
            for (int i = minPosition; i <= maxPosition; i++)
            {
                Vector2Int position = new Vector2Int(i, depth);
                FOVLocalCell cell = new FOVLocalCell(position);
                cells.Add(cell);
            }
            return cells;
        }

        public Row GetNext()
        {
            return new Row(depth + 1, startSlope, endSlope);
        }
    }
}