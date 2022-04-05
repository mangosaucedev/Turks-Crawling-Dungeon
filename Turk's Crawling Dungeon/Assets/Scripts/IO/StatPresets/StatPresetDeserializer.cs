using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            statPreset.strength = float.Parse(EvaluateNode(node, "Strength", true), CultureInfo.InvariantCulture);
            statPreset.agility = float.Parse(EvaluateNode(node, "Agility", true), CultureInfo.InvariantCulture);
            statPreset.constitution = float.Parse(EvaluateNode(node, "Constitution", true), CultureInfo.InvariantCulture);
            statPreset.cunning = float.Parse(EvaluateNode(node, "Cunning", true), CultureInfo.InvariantCulture);
            statPreset.willpower = float.Parse(EvaluateNode(node, "Willpower", true), CultureInfo.InvariantCulture);
            statPreset.charm = float.Parse(EvaluateNode(node, "Charm", true), CultureInfo.InvariantCulture);
            Assets.Add(statPreset.name, statPreset);
        }
    }
}
