using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.Zones.Environments
{
    public abstract class EnvironmentCell
    {
        /*
        public int x; 
        public int y;
        private List<EnvironmentCell> neighbors;

        protected IZone Zone => CurrentZoneInfo.zone;

        protected ChamberCellType CellType => Zone.CellTypes[x, y];
        
        public abstract string Name { get; }

        public EnvironmentCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract void Furnish();    

        public bool IsBlocked()
        {
            Cell cell = CurrentZoneInfo.grid[x, y];
            return cell.objects.Count > 0;
        }

        public bool ContainsPart<T>(Predicate<T> predicate = null) where T : Part
        {
            Cell cell = CurrentZoneInfo.grid[x, y];
            foreach (BaseObject obj in cell.objects)
            {
                if (obj.parts.TryGet(out T part) && (predicate == null || predicate(part)))
                    return true;
            }
            return false;
        }

        public bool IsDoor() => ContainsPart<Door>(d => !d.IsLocked || d.IsOpen);

        public List<EnvironmentCell> GetNeighbors()
        {
            if (neighbors == null)
                neighbors = ListNeighbors();
            return neighbors;
        }

        private List<EnvironmentCell> ListNeighbors()
        {
            neighbors = new List<EnvironmentCell>();
            GameGrid grid = CurrentZoneInfo.grid;
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    int checkX = x + xOffset;
                    int checkY = y + yOffset;
                    if (!grid.IsWithinBounds(checkX, checkY) || (checkX == x && checkY == y))
                        continue;
                    EnvironmentCell environmentCell = Zone.Environments[checkX, checkY];
                    if (environmentCell != null)
                        neighbors.Add(environmentCell);
                }
            return neighbors;
        }

        protected bool HasSurroundingFreeSpace(int xRadius, int yRadius)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            int xMin = -xRadius;
            int xMax = xRadius;
            int yMin = -yRadius;
            int yMax = yRadius;
            for (int xOffset = xMin; xOffset <= xMax; xOffset++)
                for (int yOffset = yMin; yOffset <= yMax; yOffset++)
                {
                    int checkX = x + xOffset;
                    int checkY = y + yOffset;
                    if (!grid.IsWithinBounds(checkX, checkY) || (checkX == x && checkY == y))
                        continue;
                    EnvironmentCell environmentCell = Zone.Environments[checkX, checkY];
                    if (environmentCell != null && environmentCell.IsBlocked())
                        return false;
                }
            return true;
        }

        protected bool SpaceClear(int xMin, int xMax, int yMin, int yMax)
        {
            xMin += x;
            xMax += x;
            yMin += y;
            yMax += y;
            GameGrid grid = CurrentZoneInfo.grid;
            for (int checkX = xMin; checkX <= xMax; checkX++)
                for (int checkY = yMin; checkY <= yMax; checkY++)
                {

                    if (!grid.IsWithinBounds(checkX, checkY) || (checkX == x && checkY == y))
                        continue;
                    EnvironmentCell environmentCell = Zone.Environments[checkX, checkY];
                    if (environmentCell != null && environmentCell.IsBlocked())
                        return false;
                }
            return true;
        }
        */
    }
}
