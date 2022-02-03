using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Cinematics.Dialogue;
using TCD.Texts;

namespace TCD.IO
{
    public class DialogueNodeDeserializer : RawDeserializer
    {
        private DialogueNode currentDialogueNode;

        public override string RawPath => "DialogueNodes";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("DialogueNodes");
            XmlNodeList dialogueNodes = root.SelectNodes("DialogueNode");
            foreach (XmlNode dialogueNode in dialogueNodes)
                DeserializeDialogueNode(dialogueNode);
        }

        private void DeserializeDialogueNode(XmlNode node)
        {
            currentDialogueNode = new DialogueNode();
            currentDialogueNode.name = EvaluateAttribute(node, "Name", true);
            currentDialogueNode.speakerName = EvaluateAttribute(node, "Speaker", true);
            currentDialogueNode.goToNodeName = EvaluateAttribute(node, "GoToNode");
            currentDialogueNode.isOneShot = bool.Parse(EvaluateAttribute(node, "OneShot") ?? "False");
            currentDialogueNode.text = new GameText(EvaluateNode(node, "Text", true));

            XmlNodeList responseNodes = node.SelectNodes("Response");
            foreach (XmlNode responseNode in responseNodes)
                DeserializeResponse(responseNode);

            Assets.Add(currentDialogueNode.name, currentDialogueNode);
        }

        private void DeserializeResponse(XmlNode node)
        {
            DialogueResponse response = new DialogueResponse();
            response.goToNodeName = EvaluateAttribute(node, "GoToNode");
            response.text = new GameText(node.InnerText);

            currentDialogueNode.responses.Add(response);
        }
    }
}
