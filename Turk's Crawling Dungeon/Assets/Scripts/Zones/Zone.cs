using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;
using TCD.Zones.Environments;
using Environment = TCD.Zones.Environments.Environment;

namespace TCD.Zones
{
    public class Zone : IZone
    {
        public string name;
        public string cinematicName;
        public string zoneParamsName;
        public string zoneEncountersName;
        public float encounterDensity;
        public string zoneEnvironmentsName;

        private IZoneParams zoneParams;
        private ZoneEnvironments zoneEnvironments; 
        private ZoneTerrain zoneTerrain; 
        private ZoneEncounters zoneEncounters; 
        private List<IFeature> features = new List<IFeature>();
        private List<IChamber> chambers = new List<IChamber>();
        private List<ICorridor> corridors = new List<ICorridor>();
        private TGrid<ChamberCellType> cellTypes;
        private TGrid<Environment> environments;
        private List<string> customGeneratorMachineNames = new List<string>();
        private List<ZoneGeneratorMachine> customGeneratorMachines = new List<ZoneGeneratorMachine>();

        public Cinematic Cinematic
        {
            get
            {
                if (string.IsNullOrEmpty(cinematicName))
                    return null;
                return Assets.Get<Cinematic>(cinematicName);
            }
        }

        public IZoneParams ZoneParams
        {
            get
            {
                if (zoneParams == null)
                    zoneParams = Assets.Get<IZoneParams>(zoneParamsName);
                return zoneParams;
            }
            set => zoneParams = value;
        }

        public ZoneEnvironments ZoneEnvironments
        {
            get
            {
                if (zoneEnvironments == null)
                    zoneEnvironments = Assets.Get<ZoneEnvironments>(zoneEnvironmentsName);
                return zoneEnvironments;
            }
            set => zoneEnvironments = value;
        }

        public ZoneTerrain ZoneTerrain
        {
            get
            {
                if (zoneTerrain == null)
                    zoneTerrain = Assets.Get<ZoneTerrain>(name);
                return zoneTerrain;
            }
            set => zoneTerrain = value;
        }

        public ZoneEncounters ZoneEncounters
        {
            get
            {
                if (zoneEncounters == null)
                    zoneEncounters = new ZoneEncounters();
                return zoneEncounters;
            }
            set => zoneEncounters = value;
        }

        public int Width => ZoneParams.Width;

        public int Height => ZoneParams.Height;

        public List<IFeature> Features => features;

        public List<IChamber> Chambers => chambers;

        public List<ICorridor> Corridors => corridors;

        public TGrid<ChamberCellType> CellTypes
        {
            get
            {
                if (cellTypes == null)
                    cellTypes = new TGrid<ChamberCellType>(Width, Height);
                return cellTypes;
            }
        }

        public TGrid<Environment> Environments
        {
            get
            {
                if (environments == null)
                    environments = new TGrid<Environment>(Width, Height);
                return environments;
            }
        }

        public List<string> CustomGeneratorMachineNames
        {
            get => customGeneratorMachineNames;
            set => customGeneratorMachineNames = value;
        }

        public Zone()
        {

        }

        public Zone(IZoneParams zoneParams) : this()
        {
            this.zoneParams = zoneParams;
            cellTypes = new TGrid<ChamberCellType>(Width, Height);
            environments = new TGrid<Environment>(Width, Height);
        }

        public List<IFeature> GetUndesignatedFeatures()
        {
            List<IFeature> undesignatedFeatures = new List<IFeature>();
            foreach (IFeature feature in Features)
            {
                if (feature.Environment == null)
                    undesignatedFeatures.Add(feature);
            }
            return undesignatedFeatures;
        }

        public List<ZoneGeneratorMachine> GetCustomGeneratorMachines()
        {
            if (customGeneratorMachines.Count == 0 && CustomGeneratorMachineNames.Count > 0)
            {
                foreach (string name in CustomGeneratorMachineNames)
                {
                    Type type = TypeResolver.ResolveType("TCD.Zones." + name);
                    ZoneGeneratorMachine generatorMachine = (ZoneGeneratorMachine) Activator.CreateInstance(type);
                    customGeneratorMachines.Add(generatorMachine);
                }
            }
            return customGeneratorMachines;
        }
    }
}
