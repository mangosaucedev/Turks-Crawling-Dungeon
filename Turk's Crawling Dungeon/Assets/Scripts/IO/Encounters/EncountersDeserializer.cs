using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.IO
{
    [AssetLoader]
    public class EncountersDeserializer : RawDeserializer
    {
        private Encounter currentEncounter;

        public override string RawPath => "Encounters";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Encounters");
            XmlNodeList encounterNodes = root.SelectNodes("Encounter");
            foreach (XmlNode encounterNode in encounterNodes)
                DeserializeEncounter(encounterNode);
        }

        private void DeserializeEncounter(XmlNode node)
        {
            currentEncounter = new Encounter();

            currentEncounter.name = EvaluateAttribute(node, "Name", true);
            currentEncounter.tier = int.Parse(EvaluateAttribute(node, "Tier") ?? "0");
            string density = EvaluateAttribute(node, "Density") ?? "Adjacent";
            currentEncounter.density = (EncounterDensity) Enum.Parse(typeof(EncounterDensity), density);

            XmlNodeList encounterObjectNodes = node.SelectNodes("Object");
            foreach (XmlNode encounterObjectNode in encounterObjectNodes)
                DeserializeEncounterObject(encounterObjectNode);

            Assets.Add(currentEncounter.name, currentEncounter);
        }

        private void DeserializeEncounterObject(XmlNode node)
        {
            EncounterObject obj = new EncounterObject();
            obj.name = EvaluateAttribute(node, "Name", true);
            string count = EvaluateAttribute(node, "Count", true);
            string[] counts = count.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            obj.min = int.Parse(counts[0]);
            if (counts.Length > 1)
                obj.max = int.Parse(counts[1]);
            else
                obj.max = obj.min;
            obj.chanceIn100 = int.Parse(EvaluateAttribute(node, "ChanceIn100", true));
            obj.forced = bool.Parse(EvaluateAttribute(node, "Forced") ?? "False");
            obj.exclusive = bool.Parse(EvaluateAttribute(node, "Exclusive") ?? "False");
            obj.encounter = bool.Parse(EvaluateAttribute(node, "Encounter") ?? "False");
            currentEncounter.objects.Add(obj);
        }
    }
}
