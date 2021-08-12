using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TCD.IO
{
    public class DamageTypeDeserializer : RawDeserializer
    {
        public override string RawPath => "DamageTypes";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("DamageTypes");
            XmlNodeList damageTypeNodes = root.SelectNodes("DamageType");
            foreach (XmlNode damageTypeNode in damageTypeNodes)
                DeserializeDamageType(damageTypeNode);
        }

        private void DeserializeDamageType(XmlNode node)
        {
            DamageType damageType = new DamageType();
            damageType.name = EvaluateAttribute(node, "Name", true);
            damageType.armorSoak = float.Parse(EvaluateAttribute(node, "ArmorSoak") ?? "0");
            damageType.armorReduction = float.Parse(EvaluateAttribute(node, "ArmorReduction") ?? "0");
            damageType.psiSoak = float.Parse(EvaluateAttribute(node, "PsiSoak") ?? "0");
            damageType.psiReduction = float.Parse(EvaluateAttribute(node, "PsiReduction") ?? "0");
            damageType.undodgeable = bool.Parse(EvaluateAttribute(node, "Undodgeable") ?? "False");
            Assets.Add(damageType.name, damageType);
        }
    }
}
