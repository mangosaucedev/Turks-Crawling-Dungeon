using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Zones.Utilities;

namespace TCD.Zones.Environments
{
    public class Mundane //: EnvironmentCell
    {
        /*
        public override string Name => "Mundane";

        public Mundane(int x, int y) : base(x, y)
        {

        }

        public override void Furnish()
        {
            bool isDoor = IsDoor();
            if (IsBlocked() && !isDoor)
                FurnishWall();
            if (isDoor)
                FurnishDoor();
            if (!IsBlocked())
                FurnishFloor();
        }

        private void FurnishWall()
        {
            
        }

        private int GetBlockedNeighborCount()
        {
            int blockedNeighbors = 0;
            foreach (EnvironmentCell neighbor in GetNeighbors())
            {
                if (neighbor.IsBlocked())
                    blockedNeighbors++;
            }
            return blockedNeighbors;
        }

        private void FurnishDoor()
        {

        }

        private void FurnishFloor()
        {
            int blockedNeighbors = GetBlockedNeighborCount();
            if (blockedNeighbors == 0)
                FurnishClearSpace();
            if (blockedNeighbors == 3)
                ObjectFactory.BuildFromBlueprint("GenericEnemy", new Vector2Int(x, y));
        }

        private void FurnishClearSpace()
        {
            int random = RandomInfo.Random.Next(0, 8);
            if (random < 1 && HasSurroundingFreeSpace(2, 2))
                ObjectFactory.BuildFromBlueprint(Choose.Random("GenericEnemy", "EyeTv", "TurkArm"), new Vector2Int(x, y));
            else if (random < 2 && HasSurroundingFreeSpace(1, 1))
                ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(x, y));
            else if (random < 3)
                ObjectFactory.BuildFromBlueprint(Choose.Random("TurkVessel", "MetalBarrel", "MetalShelves"), new Vector2Int(x, y));
            else if(random < 4)
                MakeBush();
            else if (random < 6)
                MakeLake();
            else if (random < 7)
                PlaceItem();
            else if (random < 8 && HasSurroundingFreeSpace(2,2))
                PlaceTableAndChair();
        }

        private void MakeBush()
        {
            int bushCount = RandomInfo.Random.Next(8, 18);
            int checkX = x;
            int checkY = y;
            GameGrid grid = CurrentZoneInfo.grid;
            string[] objs = Choose.Random(
                new string[] {"DeadBush", "RoseThorns"}, 
                new string[] {"DeadGrassLarge", "DeadGrassMedium"});
            for (int i = 0; i < bushCount; i++)
            {
                string obj = Choose.Random(objs);
                if (!grid.IsWithinBounds(checkX, checkY))
                    continue;
                EnvironmentCell environmentCell = Zone.Environments[checkX, checkY];
                if (!environmentCell.IsBlocked())
                    ObjectFactory.BuildFromBlueprint(obj, new Vector2Int(checkX, checkY));
                checkX += RandomInfo.Random.Next(-1, 2);
                checkY += RandomInfo.Random.Next(-1, 2);
            }
        }

        private void MakeLake()
        {
            int width = RandomInfo.Random.Next(8, 15);
            int height = RandomInfo.Random.Next(8, 15);
            GameGrid grid = CurrentZoneInfo.grid;
            CellularAutomataGrid cellularAutomataGrid = new CellularAutomataGrid(width, height, 55f, 3);
            string liquid = Choose.Random("Water10", "Acid10");
            HashSet<Vector2Int> shape = CellularAutomataUtility.GetLargestContiguousShape(cellularAutomataGrid);
            foreach (Vector2Int lakePosition in shape)
            {
                Vector2Int position = new Vector2Int(x, y) + lakePosition;
                if (!grid.IsWithinBounds(position) ||
                    (grid[position].Contains(out Obstacle obstacle) && obstacle.IsImpassable))
                    continue;
                ObjectFactory.BuildFromBlueprint(liquid, position);
            }
        }

        private void PlaceItem()
        {
            string item = Choose.Random(
                "JournalEntryA", 
                "JournalEntryB", 
                "JournalEntryC", 
                "JournalEntryD", 
                "JournalEntryE", 
                "JournalEntryF", 
                "JournalEntryG", 
                "JournalEntryH");
            ObjectFactory.BuildFromBlueprint(item, new Vector2Int(x, y));
        }

        private void PlaceTableAndChair()
        {
            ObjectFactory.BuildFromBlueprint("GenericTable", new Vector2Int(x, y));
            int random = RandomInfo.Random.Next(0, 2);
            if (random < 1)
            {
                ObjectFactory.BuildFromBlueprint("GenericChair", new Vector2Int(x + 1, y));
                ObjectFactory.BuildFromBlueprint("GenericChair", new Vector2Int(x - 1, y));
            }
            else
            {
                ObjectFactory.BuildFromBlueprint("GenericChair", new Vector2Int(x, y - 1));
                ObjectFactory.BuildFromBlueprint("GenericChair", new Vector2Int(x, y + 1));
            }
        }
        */
    }
}
