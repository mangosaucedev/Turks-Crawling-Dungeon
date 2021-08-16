using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Encounters;

namespace TCD.IO
{
    public class EncountersDeserializer : RawDeserializer
    {
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
            Encounter encounter = new Encounter();
            encounter.name = EvaluateAttribute(node, "Name", true);
            Assets.Add(encounter.name, encounter);
        }
    }
}
