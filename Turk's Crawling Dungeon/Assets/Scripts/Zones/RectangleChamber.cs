using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class RectangleChamber : Chamber
    {
        public RectangleChamber(int width, int height) : 
            base(width, height)
        {

        }

        public override void Generate()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Cells[x, y] = ChamberCellType.Floor;
        }
    }
}
