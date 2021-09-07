using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Zones;

namespace TCD.IO
{
    public class ZoneEncountersDeserializer : RawDeserializer
    {
        public override string RawPath => "ZoneEncounters";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("ZoneEncounters");
            XmlNodeList zoneEncounterNodes = root.SelectNodes("ZoneEncounter");
            foreach (XmlNode zoneEncounterNode in zoneEncounterNodes)
                DeserializeZoneEncounter(zoneEncounterNode);
        }

        private void DeserializeZoneEncounter(XmlNode node)
        {
            ZoneEncounters zoneEncounters = new ZoneEncounters();
            zoneEncounters.name = EvaluateAttribute(node, "Name", true);
            Assets.Add(zoneEncounters.name, zoneEncounters);
        }
    }
}
