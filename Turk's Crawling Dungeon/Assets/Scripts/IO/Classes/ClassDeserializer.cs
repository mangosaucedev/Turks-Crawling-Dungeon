using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.IO
{
    public class ClassDeserializer : RawDeserializer
    {
        public override string RawPath => "Classes";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Classes");
            XmlNodeList classNodes = root.SelectNodes("Class");
            foreach (XmlNode classNode in classNodes)
                DeserializeClass(classNode);
        }

        private void DeserializeClass(XmlNode node)
        {
            Class currentClass = new Class();
            currentClass.name = EvaluateAttribute(node, "Name", true);
            currentClass.statPresetName = EvaluateNode(node, "StatPreset", true);
            
            XmlNodeList talentTreeNodes = node.SelectNodes("TalentTree");
            foreach (XmlNode talentTreeNode in talentTreeNodes)
                currentClass.talentTreeNames.Add(talentTreeNode.InnerText);

            Assets.Add(currentClass.name, currentClass);
        }
    }
}
