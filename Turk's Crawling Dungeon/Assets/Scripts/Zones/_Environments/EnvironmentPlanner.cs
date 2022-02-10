using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Environments
{
    public class EnvironmentPlanner : ZoneGeneratorMachine
    {
        private ZoneEnvironments ZoneEnvironments => Zone.ZoneEnvironments;

        public override IEnumerator Generate()
        {
            List<ZoneEnvironment> unplacedForcedZoneEnvironments = ZoneEnvironments.GetUnplacedForcedZoneEnvironments();
            foreach (ZoneEnvironment zoneEnvironment in unplacedForcedZoneEnvironments)
                AssignEnvironment(zoneEnvironment);         
            foreach (IFeature feature in Zone.GetUndesignatedFeatures())
                AssignEnvironmentToFeature(feature);
            foreach (IFeature feature in Zone.Features)
                PlanFeatureEnvironment(feature);
            yield break;
        }

        private void AssignEnvironment(ZoneEnvironment zoneEnvironment)
        {
            EnvironmentPlacement placement = zoneEnvironment.placement;
            IFeature feature = null;
            switch (placement)
            {
                case EnvironmentPlacement.Random:
                    List<IFeature> undesignatedFeatures = Zone.GetUndesignatedFeatures();
                    if (undesignatedFeatures.Count == 0)
                        return;
                    feature = Choose.Random(undesignatedFeatures);
                    SetFeatureEnvironment(feature, zoneEnvironment);
                    break;
                case EnvironmentPlacement.Start:
                    if (Zone.Chambers.Count > 0)
                        feature = Zone.Chambers[0];
                    else 
                        feature = Zone.Features[0];
                    SetFeatureEnvironment(feature, zoneEnvironment);
                    break;
                case EnvironmentPlacement.Final:
                    if (Zone.Chambers.Count > 0)
                        feature = Zone.Chambers[Zone.Chambers.Count - 1];
                    else
                        feature = Zone.Features[Zone.Features.Count - 1];
                    SetFeatureEnvironment(feature, zoneEnvironment);
                    break;
            }    
        }
        
        private void SetFeatureEnvironment(IFeature feature, ZoneEnvironment zoneEnvironment)
        {
            Environment environment = zoneEnvironment.environment;
            feature.Environment = environment;
            ZoneEnvironments.SetPlaced(zoneEnvironment);
        }

        private void AssignEnvironmentToFeature(IFeature feature)
        {
            List<ZoneEnvironment> nonExcludedZoneEnvironments = ZoneEnvironments.GetNonExludedZoneEnvironments();
            if (nonExcludedZoneEnvironments.Count == 0)
                ExceptionHandler.Handle(new Exception("Not enough non-exclusive environments to furnish Zone! (EnvironmentPlanner)"));
            using (GrabBag<ZoneEnvironment> bag = new GrabBag<ZoneEnvironment>())
            {
                foreach(ZoneEnvironment zoneEnvironment in nonExcludedZoneEnvironments)
                    bag.AddItem(zoneEnvironment, zoneEnvironment.weight);
                ZoneEnvironment chosenZoneEnvironment = bag.Grab();
                SetFeatureEnvironment(feature, chosenZoneEnvironment);
            }
        }

        private void PlanFeatureEnvironment(IFeature feature)
        {
            int xMin = feature.BoundsInt.xMin;
            int xMax = feature.BoundsInt.xMax;
            int yMin = feature.BoundsInt.yMin;
            int yMax = feature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (!Zone.CellTypes.IsWithinBounds(x, y))
                        continue;
                    if (Zone.CellTypes[x, y] != ChamberCellType.None)
                        Zone.Environments[x, y] = feature.Environment;
                }
        }
    }
}
