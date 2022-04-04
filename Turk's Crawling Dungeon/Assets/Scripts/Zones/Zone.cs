using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;
using TCD.Zones.Environments;

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
            get => zoneParams;
            set => zoneParams = value;
        }

        public ZoneEnvironments ZoneEnvironments
        {
            get => zoneEnvironments;
            set => zoneEnvironments = value;
        }

        public ZoneTerrain ZoneTerrain
        {
            get => zoneTerrain;
            set => zoneTerrain = value;
        }

        public ZoneEncounters ZoneEncounters
        {
            get => zoneEncounters;
            set => zoneEncounters = value;
        }

        public int Width => ZoneParams.Width;

        public int Height => ZoneParams.Height;

        public List<IFeature> Features => features;

        public List<IChamber> Chambers => chambers;

        public List<ICorridor> Corridors => corridors;

        public TGrid<ChamberCellType> CellTypes => cellTypes;

        public TGrid<Environment> Environments => environments;

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
    }
}
