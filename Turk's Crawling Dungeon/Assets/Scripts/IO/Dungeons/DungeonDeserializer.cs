using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Zones.Dungeons;

namespace TCD.IO
{
    [AssetLoader]
    public class DungeonDeserializer : RawDeserializer
    {
        public override string RawPath => "Dungeons";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Dungeons");
            XmlNodeList dungeonNodes = root.SelectNodes("Dungeon");
            foreach (XmlNode dungeonNode in dungeonNodes)
                DeserializeDungeon(dungeonNode);
        }

        private void DeserializeDungeon(XmlNode node)
        {
            Dungeon dungeon = new Dungeon();
            dungeon.name = EvaluateAttribute(node, "Name", true);

            XmlNodeList zoneNodeList = node.SelectNodes("Zone");
            foreach (XmlNode zoneNode in zoneNodeList)
                dungeon.zoneNames.Add(zoneNode.InnerText);

            Assets.Add(dungeon.name, dungeon);
        }
    }
}
