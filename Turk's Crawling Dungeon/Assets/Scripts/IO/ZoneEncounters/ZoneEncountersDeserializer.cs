using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Encounters;
using TCD.Zones;

namespace TCD.IO
{
    [AssetLoader]
    public class ZoneEncountersDeserializer : RawDeserializer
    {
        ZoneEncounters currentZoneEncounters;

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
            currentZoneEncounters = new ZoneEncounters();
            currentZoneEncounters.name = EvaluateAttribute(node, "Name", true);
            XmlNodeList encounterNodes = node.SelectNodes("Encounter");
            foreach (XmlNode encounterNode in encounterNodes)
                DeserializeEncounter(encounterNode);
            Assets.Add(currentZoneEncounters.name, currentZoneEncounters);
        }

        private void DeserializeEncounter(XmlNode node)
        {
            ZoneEncounter zoneEncounter = new ZoneEncounter();
            zoneEncounter.name = EvaluateAttribute(node, "Name", true);
            string typeName = EvaluateAttribute(node, "Type", true);
            EncounterType type = (EncounterType) Enum.Parse(typeof(EncounterType), typeName);
            zoneEncounter.type = type;
            zoneEncounter.weight = int.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            currentZoneEncounters.buildEncounters.Add(zoneEncounter);
        }
    }
}
