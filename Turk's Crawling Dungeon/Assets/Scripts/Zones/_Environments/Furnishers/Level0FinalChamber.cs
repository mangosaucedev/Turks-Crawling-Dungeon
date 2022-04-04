using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones.Environments
{
    public class Level0FinalChamber : Furnisher
    {
        public override void Furnish(IFeature feature, int x, int y)
        {
            base.Furnish(feature, x, y);
            Cell cell = CurrentZoneInfo.grid[x, y];
            for (int i = cell.Objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = cell.Objects[i];
                if (obj)
                {
                    obj.cell.SetPosition(0, 0);
                    Object.Destroy(obj.gameObject);
                }
            }
            ObjectFactory.BuildFromBlueprint("DownStairs", new Vector2Int(x, y));
        }
    }
}
