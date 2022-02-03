using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Cinematics.Dialogue;

namespace TCD.IO
{
    public class DialogueSpeakerDeserializer : RawDeserializer
    {
        public override string RawPath => "DialogueSpeakers";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("DialogueSpeakers");
            XmlNodeList speakerNodes = root.SelectNodes("DialogueSpeaker");
            foreach (XmlNode speakerNode in speakerNodes)
                DeserializeDialogueSpeaker(speakerNode);
        }

        private void DeserializeDialogueSpeaker(XmlNode node)
        {
            DialogueSpeaker dialogueSpeaker = new DialogueSpeaker();
            dialogueSpeaker.name = EvaluateAttribute(node, "Name", true);
            dialogueSpeaker.displayName = EvaluateAttribute(node, "DisplayName", true);
            dialogueSpeaker.colorName = EvaluateAttribute(node, "Color", true);
            dialogueSpeaker.portraitName = EvaluateAttribute(node, "Portrait", true);
            Assets.Add(dialogueSpeaker.name, dialogueSpeaker);
        }
    }
}
