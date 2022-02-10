using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.IO
{
    public class StatPresetDeserializer : RawDeserializer
    {
        public override string RawPath => "StatPresets";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("StatPresets");
            XmlNodeList statPresetNodes = root.SelectNodes("StatPreset");
            foreach (XmlNode statPresetNode in statPresetNodes)
                DeserializeStatPreset(statPresetNode);
        }

        private void DeserializeStatPreset(XmlNode node)
        {
            StatPreset statPreset = new StatPreset();
            statPreset.name = EvaluateAttribute(node, "Name", true);
            statPreset.strength = float.Parse(EvaluateNode(node, "Strength", true));
            statPreset.agility = float.Parse(EvaluateNode(node, "Agility", true));
            statPreset.constitution = float.Parse(EvaluateNode(node, "Constitution", true));
            statPreset.cunning = float.Parse(EvaluateNode(node, "Cunning", true));
            statPreset.willpower = float.Parse(EvaluateNode(node, "Willpower", true));
            statPreset.charm = float.Parse(EvaluateNode(node, "Charm", true));
            Assets.Add(statPreset.name, statPreset);
        }
    }
}
