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
            XmlNode rootNode = xml.SelectSingleNode("Texts");
            XmlNodeList textNodes = rootNode.SelectNodes("Text");
            foreach (XmlNode textNode in textNodes)
                DeserializeText(textNode);
        }

        private void DeserializeText(XmlNode node)
        {
            currentText = new GameText();
            currentText.name = EvaluateAttribute(node, "Name", true);
            XmlNodeList pageNodes = node.SelectNodes("Page");
            foreach (XmlNode pageNode in pageNodes)
                currentText.pages.Add(pageNode.InnerText);
            Assets.Add(currentText.name, currentText);
        }
    }
}
