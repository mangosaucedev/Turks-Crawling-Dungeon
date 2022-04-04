using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Zones.Environments;

namespace TCD.IO
{
    [AssetLoader]
    public class ZoneEnvironmentsDeserializer : RawDeserializer
    {
        private ZoneEnvironments currentZoneEnvironments;

        public override string RawPath => "ZoneEnvironments";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("ZoneEnvironments");
            XmlNodeList zoneEnvironmentsNodes = root.SelectNodes("ZoneEnvironment");
            foreach (XmlNode zoneEnvironmentsNode in zoneEnvironmentsNodes)
                DeserializeZoneEnvironments(zoneEnvironmentsNode);
        }

        private void DeserializeZoneEnvironments(XmlNode node)
        {
            currentZoneEnvironments = new ZoneEnvironments();
            currentZoneEnvironments.name = EvaluateAttribute(node, "Name", true);
            XmlNodeList environmentNodes = node.SelectNodes("Environment");
            foreach (XmlNode environmentNode in environmentNodes)
                DeserializeEnvironment(environmentNode);
            Assets.Add(currentZoneEnvironments.name, currentZoneEnvironments);
        }

        private void DeserializeEnvironment(XmlNode node)
        {
            ZoneEnvironment environment = new ZoneEnvironment();
            environment.name = EvaluateAttribute(node, "Name", true);
            environment.weight = int.Parse(EvaluateNode(node, "Weight") ?? "1");
            environment.exclusive = bool.Parse(EvaluateNode(node, "Exclusive") ?? "False");
            environment.forced = bool.Parse(EvaluateNode(node, "Forced") ?? "False");
            string placementString = EvaluateNode(node, "Placement") ?? "Random";
            environment.placement = (EnvironmentPlacement) Enum.Parse(typeof(EnvironmentPlacement), placementString);
            currentZoneEnvironments.environmentReferences.Add(environment);
        }
    }
}
