using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Utilities
{
    public class CellularAutomataGrid : SeededGrid<bool>
    {
        private const bool FILLED = false;
        private const bool EMPTY = true;
        private const float DEFAULT_FILL_CHANCE = 40f;
        private const int ADJACENT_CELLS_TO_EMPTY = 3;
        private const int ADJACENT_CELLS_TO_FILL = 4;

        public float randomFillChance = DEFAULT_FILL_CHANCE;
        private int smoothSteps;

        public CellularAutomataGrid(int width, int height) :
            base(width, height)
        {
            Fill();
        }

        public CellularAutomataGrid(int width, int height, float randomFillChance, int smoothSteps = 5) :
            this(width, height)
        {
            this.randomFillChance = randomFillChance;
            this.smoothSteps = smoothSteps;
            Fill();
            for (int i = 0; i < smoothSteps; i++)
                SmoothGrid();
        }

        protected override void Fill()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    FillCell(x, y);
                }
        }

        protected override void FillCell(int x, int y)
        {
            float randomChance = Random.Next(0, 100);

            bool fillCell = randomChance < randomFillChance;
            bool value = (fillCell) ? FILLED : EMPTY;
            Set(x, y, value);
        }

        private void SmoothGrid()
        {
            CellularAutomataGrid newGrid = new CellularAutomataGrid(width, height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    SmoothCell(x, y, newGrid);
                }

            CopyFrom(newGrid);
        }

        private void SmoothCell(int x, int y, CellularAutomataGrid newGrid)
        {
            int adjacentWallCount = GetAdjacentWallCount(x, y);
            bool cell = Get(x, y);
            bool isWall = cell == FILLED;

            if (isWall)
            {
                bool emptyCell = adjacentWallCount < ADJACENT_CELLS_TO_EMPTY;
                if (emptyCell)
                    newGrid[x, y] = EMPTY;
                else
                    newGrid[x, y] = FILLED;
            }
            else
            {
                bool placeWall = adjacentWallCount > ADJACENT_CELLS_TO_FILL;
                if (placeWall)
                    newGrid[x, y] = FILLED;
                else
                    newGrid[x, y] = EMPTY;
            }
        }

        private int GetAdjacentWallCount(int x, int y)
        {
            int count = 0;

            for (int checkX = x - 1; checkX <= x + 1; checkX++)
                for (int checkY = y - 1; checkY <= y + 1; checkY++)
                {
                    bool isOriginalCell = checkX == x && checkY == y;

                    if (isOriginalCell)
                        continue;

                    bool isWithinBounds = IsWithinBounds(checkX, checkY);

                    if (isWithinBounds)
                    {
                        bool cell = Get(checkX, checkY);
                        count += cell ? 0 : 1;
                    }
                    else
                        count += 1;
                }
            return count;
        }
    }
}
