using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Cinematics.Dialogues;

namespace TCD.IO
{
    [AssetLoader]
    public class DialogueSpeakerDeserializer : RawDeserializer
    {
        public override string RawPath => "DialogueSpeakers";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {

        }

        private void DeserializeDialogueSpeaker(XmlNode node)
        {

        }
    }
}
