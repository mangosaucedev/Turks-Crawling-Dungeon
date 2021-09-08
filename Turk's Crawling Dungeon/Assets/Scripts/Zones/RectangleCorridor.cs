using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class RectangleCorridor : Corridor
    {
        public RectangleCorridor(ChamberAnchor start, ChamberAnchor end) : 
            base(start, end)
        {

        }

        public override void Generate()
        {
            x = start.XReal;
            y = start.YReal;
            if (start.direction == Cardinal.East ||
                start.direction == Cardinal.West)
            {
                PlanHorizontalCorridor();
                PlanVerticalCorridor();
            }
            else
            {
                PlanVerticalCorridor();
                PlanHorizontalCorridor();
            }
        }

        private void PlanHorizontalCorridor()
        {
            int xDirection = Mathf.RoundToInt(Mathf.Sign(end.XReal - x));
            int yOffsetMin = -width / 2;
            int yOffsetMax = width / 2;
            while (x != end.XReal)
            {
                x += xDirection;
                for (int yOffset = yOffsetMin; yOffset <= yOffsetMax; yOffset++)
                {
                    Vector2Int position = new Vector2Int(x, y + yOffset);
                    Cells.Add(position);
                }
            }
        }

        private void PlanVerticalCorridor()
        {
            int yDirection = Mathf.RoundToInt(Mathf.Sign(end.YReal - y));
            int xOffsetMin = -width / 2;
            int xOffsetMax = width / 2;
            while (y != end.YReal)
            {
                y += yDirection;
                for (int xOffset = xOffsetMin; xOffset <= xOffsetMax; xOffset++)
                {
                    Vector2Int position = new Vector2Int(x + xOffset, y);
                    Cells.Add(position);
                }
            }
        }
        
    }
}
