using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects;

namespace TCD.IO
{
    public class ObjectsDeserializer : RawDeserializer
    {
        private static PartParser partParser = new PartParser();

        private XmlNode currentObjectNode;
        private ObjectBlueprint currentBlueprint;

        public override string RawPath => "Objects";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Objects");
            XmlNodeList objectNodes = root.SelectNodes("Object");
            foreach (XmlNode objectNode in objectNodes)
                DeserializeAndIndexObjectBlueprint(objectNode);
        }

        private void DeserializeAndIndexObjectBlueprint(XmlNode node)
        {
            currentObjectNode = node;
            ObjectBlueprint blueprint = TryToDeserializeObjectBlueprint();
            if (blueprint != null)
                IndexBlueprint(blueprint);
        }

        private ObjectBlueprint TryToDeserializeObjectBlueprint()
        {
            currentBlueprint = new ObjectBlueprint();
            ParseBlueprintNodeAttributes();
            ParseBlueprintParts();
            ParseBlueprintOverrideParts();
            ParseBlueprintRemoveParts();
            return currentBlueprint;
        }

        private void ParseBlueprintNodeAttributes()
        {
            currentBlueprint.name = 
                EvaluateAttribute(currentObjectNode, "Name", true);

            currentBlueprint.displayName =
                EvaluateNode(currentObjectNode, "DisplayName") ?? "Nameless Object";

            currentBlueprint.displayNamePlural =
                EvaluateNode(currentObjectNode, "DisplayNamePlural") ?? currentBlueprint.displayName + "s";

            currentBlueprint.description =
                EvaluateNode(currentObjectNode, "Description") ?? "Disgustingly typical.";

            currentBlueprint.faction =
                EvaluateNode(currentObjectNode, "Faction") ?? "Neutral";

            currentBlueprint.size =
                EvaluateNode(currentObjectNode, "Size") ?? "1x1";

            currentBlueprint.hpMax =
                EvaluateNode(currentObjectNode, "HpMax") ?? "1";

            currentBlueprint.hp = currentBlueprint.hpMax;

            currentBlueprint.value =
                EvaluateNode(currentObjectNode, "Value") ?? "0.05";

            currentBlueprint.inheritsFrom = 
                EvaluateAttribute(currentObjectNode, "InheritsFrom");
        }

        private void ParseBlueprintParts()
        {
            XmlNodeList partNodes = currentObjectNode.SelectNodes("Part");
            foreach (XmlNode node in partNodes)
            {
                PartBlueprint part = partParser.ParseFromNode(node);
                currentBlueprint.AddPart(part);
            }
        }

        private void ParseBlueprintOverrideParts()
        {
            XmlNodeList partNodes = currentObjectNode.SelectNodes("OverridePart");
            foreach (XmlNode node in partNodes)
            {
                PartBlueprint part = partParser.ParseFromNode(node);
                currentBlueprint.AddOverridePart(part);
            }
        }


        private void ParseBlueprintRemoveParts()
        {
            XmlNodeList removePartNodes = currentObjectNode.SelectNodes("RemovePart");
            foreach (XmlNode node in removePartNodes)
            {
                string name = node.Attributes["Name"]?.InnerText;
                currentBlueprint.AddRemovePart(name);
            }
        }

        private void IndexBlueprint(ObjectBlueprint blueprint)
        {
            Assets.Add(blueprint.name, blueprint);
#if UNITY_EDITOR
            DebugRawViewer debugRawViewer = ServiceLocator.Get<DebugRawViewer>();
            debugRawViewer.objectBlueprints.Add(blueprint);
#endif
        }
    }
}
