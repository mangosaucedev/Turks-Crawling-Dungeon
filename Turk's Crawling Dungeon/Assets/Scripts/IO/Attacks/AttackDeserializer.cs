using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.IO
{
    [AssetLoader]
    public class AttackDeserializer : RawDeserializer
    {
        public override string RawPath => "Attacks";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Attacks");
            XmlNodeList attackNodes = root.SelectNodes("Attack");
            foreach (XmlNode attackNode in attackNodes)
                DeserializeAttack(attackNode);
        }

        private void DeserializeAttack(XmlNode node)
        {
            Attack attack = new Attack();
            attack.name = EvaluateAttribute(node, "Name", true);
            attack.verb = EvaluateNode(node, "Verb", true);
            attack.verbPastTense = EvaluateNode(node, "VerbPastTense", true);
            attack.minDamage = int.Parse(EvaluateNode(node, "MinDamage", true));
            attack.maxDamage = int.Parse(EvaluateNode(node, "MaxDamage", true));
            attack.attackTypeName = EvaluateNode(node, "AttackType") ?? "Physical";
            attack.damageTypeName = EvaluateNode(node, "DamageType", true);
            Assets.Add(attack.name, attack);
        }
    }
}
