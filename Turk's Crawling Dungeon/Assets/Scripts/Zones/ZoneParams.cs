using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    [Serializable]
    public class ZoneParams : IZoneParams
    {
        public string name;

        [Header("Zone")]
        [SerializeField] private ZoneGeneratorType type;
        [SerializeField] private int width;
        [SerializeField] private int height;
        
        [Header("Chambers")]
        [SerializeField] private int minChamberWidth;
        [SerializeField] private int minChamberHeight;
        [SerializeField] private int maxChamberWidth;
        [SerializeField] private int maxChamberHeight;
        [SerializeField] private int maxChambers;
        [SerializeField] private int minChamberChildDistance;
        [SerializeField] private int minChamberBufferDistance;

        [Header("Corridor")]
        [SerializeField] private int minCorridorWidth;
        [SerializeField] private int maxCorridorWidth;

        [Header("Caves")]
        [SerializeField] private bool generateCaves;
        [SerializeField] private int cavePenetrationChance;

        public ZoneGeneratorType Type
        {
            get => type;
            set => type = value;
        }

        public int Width
        {
            get => width;
            set => width = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }

        public int MinChamberWidth
        {
            get => minChamberWidth;
            set => minChamberWidth = value;
        }
        
        public int MinChamberHeight
        {
            get => minChamberHeight;
            set => minChamberHeight = value;
        }
        
        public int MaxChamberWidth
        {
            get => maxChamberWidth;
            set => maxChamberWidth = value;
        }
        
        public int MaxChamberHeight
        {
            get => maxChamberHeight;
            set => maxChamberHeight = value;
        }
        
        public int MaxChambers
        {
            get => maxChambers;
            set => maxChambers = value;
        }
        
        public int MinChamberChildDistance
        {
            get => minChamberChildDistance;
            set => minChamberChildDistance = value;
        }
        
        public int MinChamberBufferDistance
        {
            get => minChamberBufferDistance;
            set => minChamberBufferDistance = value;
        }
        
        public int MinCorridorWidth
        {
            get => minCorridorWidth;
            set => minCorridorWidth = value;
        }
        
        public int MaxCorridorWidth
        {
            get => maxCorridorWidth;
            set => maxCorridorWidth = value;
        }

        public bool GenerateCaves
        {
            get => generateCaves;
            set => generateCaves = value;
        }

        public int CavePenetrationChance
        {
            get => cavePenetrationChance;
            set => cavePenetrationChance = value;
        }
    }
}
