using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.IO
{
    public class TalentTreeDeserializer : RawDeserializer
    {
        public override string RawPath => "TalentTrees";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("TalentTrees");
            XmlNodeList talentTreeNodes = root.SelectNodes("TalentTree");
            foreach (XmlNode talentTreeNode in talentTreeNodes)
                DeserializeTalentTree(talentTreeNode);
        }

        private void DeserializeTalentTree(XmlNode node)
        {
            TalentTree talentTree = new TalentTree();
            
            talentTree.name = EvaluateAttribute(node, "Name", true);
            talentTree.displayName = EvaluateAttribute(node, "DisplayName", true);

            XmlNodeList talentNodes = node.SelectNodes("Talent");
            foreach (XmlNode talentNode in talentNodes)
                talentTree.talentNames.Add(talentNode.InnerText);

            Assets.Add(talentTree.name, talentTree);
        }
    }
}
