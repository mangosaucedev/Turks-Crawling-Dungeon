using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TCD.IO
{
    [AssetLoader]
    public class TileDeserializer : RawDeserializer
    {
        public override string RawPath => "Tiles";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Tiles");
            XmlNodeList tileNodes = root.SelectNodes("Tile");
            foreach (XmlNode tileNode in tileNodes)
                  DeserializeTile(tileNode);
        }

        private void DeserializeTile(XmlNode node)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.name = EvaluateAttribute(node, "Name", true);
            tile.sprite = Assets.Get<Sprite>(node.InnerText);
            Assets.Add(tile.name, (TileBase) tile);
        }
    }
}
