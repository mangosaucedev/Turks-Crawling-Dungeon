using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Zones.Dungeons;

namespace TCD.IO
{
    public class CampaignDeserializer : RawDeserializer
    {
        public override string RawPath => "Campaigns";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Campaigns");
            XmlNodeList campaignNodes = root.SelectNodes("Campaign");
            foreach (XmlNode campaignNode in campaignNodes)
                DeserializeCampaign(campaignNode);
        }

        private void DeserializeCampaign(XmlNode node)
        {
            Campaign campaign = new Campaign();
            campaign.name = EvaluateAttribute(node, "Name", true);

            XmlNodeList classNodeList = node.SelectNodes("Class");
            foreach (XmlNode classNode in classNodeList)
                campaign.classNames.Add(classNode.InnerText);

            XmlNodeList dungeonNodeList = node.SelectNodes("Dungeon");
            foreach (XmlNode dungeonNode in dungeonNodeList)
                campaign.dungeonNames.Add(dungeonNode.InnerText);

            Assets.Add(campaign.name, campaign);
        }
    }
}
