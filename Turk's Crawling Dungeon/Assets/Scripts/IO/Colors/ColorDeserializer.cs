using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TCD.IO
{
    [AssetLoader]
    public class ColorDeserializer : RawDeserializer
    {
        public override string RawPath => "Colors";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Colors");
            XmlNodeList colorNodes = root.SelectNodes("Color");
            foreach (XmlNode colorNode in colorNodes)
                DeserializeColor(colorNode);
        }

        private void DeserializeColor(XmlNode node)
        {
            string name = EvaluateAttribute(node, "Name", true);
            string symbol = EvaluateAttribute(node, "Symbol", true);
            string htmlString = node.InnerText;
            if (ColorUtility.TryParseHtmlString(htmlString, out Color color))
            {
                Assets.Add(name, color);
                Assets.Add(symbol, color);
            }
        }
    }
}
