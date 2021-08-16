using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class ZoneTerrain 
    {
        public List<Wall> walls = new List<Wall>();
        public List<Floor> floors = new List<Floor>();
        private int wallSurfaces;
        private int floorSurfaces;

        public string GetWall(float surface)
        {
            int surfaces = GetWallSurfaces();
            int index = 0;
            for (int i = surfaces; i >= 0; i--)
            {
                if ((surface * surfaces) + 0.5f >= surfaces)
                {
                    index = i;
                    break;
                }
            }
            Wall wall = GetWallForSurface(index);
            return wall.name;
        }
        
        private int GetWallSurfaces()
        {
            if (wallSurfaces == 0)
            {
                foreach (Wall wall in walls)
                {
                    if (wall.surface > wallSurfaces)
                        wallSurfaces = wall.surface;
                }
            }
            return wallSurfaces;
        }

        private Wall GetWallForSurface(int surface)
        {
            using (GrabBag<Wall> bag = new GrabBag<Wall>())
            {
                foreach (Wall wall in walls)
                {
                    if (wall.surface == surface)
                        bag.AddItem(wall, wall.weight);
                }
                return bag.Grab();
            }
        }

        public string GetFloor(float surface)
        {
            int surfaces = GetFloorSurfaces();
            int index = 0;
            for (int i = surfaces; i >= 0; i--)
            {
                if ((surface * surfaces) + 0.5f >= surfaces)
                {
                    index = i;
                    break;
                }
            }
            Floor floor = GetFloorForSurface(index);
            return floor.name;
        }

        private int GetFloorSurfaces()
        {
            if (floorSurfaces == 0)
            {
                foreach (Floor floor in floors)
                {
                    if (floor.surface > floorSurfaces)
                        floorSurfaces = floor.surface;
                }
            }
            return floorSurfaces;
        }

        private Floor GetFloorForSurface(int surface)
        {
            using (GrabBag<Floor> bag = new GrabBag<Floor>())
            {
                foreach (Floor floor in floors)
                {
                    if (floor.surface == surface)
                        bag.AddItem(floor, floor.weight);
                }
                return bag.Grab();
            }
        }
    }
}
