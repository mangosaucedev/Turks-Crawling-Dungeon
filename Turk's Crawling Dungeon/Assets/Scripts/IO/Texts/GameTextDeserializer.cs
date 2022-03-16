using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Texts;

namespace TCD.IO
{
    public class GameTextDeserializer : RawDeserializer
    {
        GameText currentText;

        public override string RawPath => "Text";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {

        }

        private void DeserializeText(XmlNode node)
        {

        }
    }
}
