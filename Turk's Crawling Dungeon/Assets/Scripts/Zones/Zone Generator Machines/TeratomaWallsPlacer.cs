using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.Zones
{
    public class TeratomaWallsPlacer : ZoneGeneratorMachine
    {
        private const int TERATOMA_DIFFICULTY = 48;

        public override IEnumerator Generate()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Cell cell = GameGrid.Current.Get(x, y);
                    if (RandomInfo.Random.Next(TERATOMA_DIFFICULTY) == 0 && cell.Contains<Objects.Parts.Wall>() && !cell.Contains<Brain>())
                        ObjectFactory.BuildFromBlueprint("TeratomaWall", new Vector2Int(x, y));
                }
            yield return null;
        }
    }
}
