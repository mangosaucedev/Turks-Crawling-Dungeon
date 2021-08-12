using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class Zone : IZone
    {
        private IZoneParams zoneParams;
        private ZoneEnvironments zoneEnvironments; 
        private List<IFeature> features = new List<IFeature>();
        private List<IChamber> chambers = new List<IChamber>();
        private List<ICorridor> corridors = new List<ICorridor>();
        private TGrid<ChamberCellType> cellTypes;
        private TGrid<Environment> environments;

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

        public int Width => ZoneParams.Width;

        public int Height => ZoneParams.Height;

        public List<IFeature> Features => features;

        public List<IChamber> Chambers => chambers;

        public List<ICorridor> Corridors => corridors;

        public TGrid<ChamberCellType> CellTypes => cellTypes;

        public TGrid<Environment> Environments => environments;

        public Zone(IZoneParams zoneParams)
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
