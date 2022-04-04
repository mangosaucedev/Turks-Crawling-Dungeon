using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Cinematics.Dialogues;
using TCD.Texts;

namespace TCD.IO
{
    [AssetLoader]
    public class DialogueNodeDeserializer : RawDeserializer
    {
        private Dialogue currentDialogueNode;

        public override string RawPath => "DialogueNodes";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {

        }

        private void DeserializeDialogueNode(XmlNode node)
        {

        }

        private void DeserializeResponse(XmlNode node)
        {

        }
    }
}
