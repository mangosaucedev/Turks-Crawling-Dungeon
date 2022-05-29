using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Templates;

namespace TCD.Zones
{
    public class TutorialPlanner : TutorialTemplateMachine
    {
        private ZoneTemplate template;

        public override IEnumerator Generate()
        {
            template = GetZoneTemplate();
            for (int x = 0; x < template.Width; x++)
                for (int y = 0; y < template.Height; y++)   
                    Plan(x, y);
            yield return null;
        }

        private void Plan(int x, int y)
        {
            var tile = template.tiles[x, y];
            if (tile >= ZoneTemplateTile.Floor)
                Zone.CellTypes[x, y] = ChamberCellType.Floor;
            if (tile == ZoneTemplateTile.Wall)
                Zone.CellTypes[x, y] = ChamberCellType.Wall;
            if (tile == ZoneTemplateTile.Door)
                Zone.CellTypes[x, y] = ChamberCellType.Door;
        }
    }
}
