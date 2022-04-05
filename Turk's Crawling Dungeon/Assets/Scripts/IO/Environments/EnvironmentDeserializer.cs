using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;
using TCD.Zones;
using TCD.Zones.Environments;
using Environment = TCD.Zones.Environments.Environment;

namespace TCD.IO
{
    [AssetLoader]
    public class EnvironmentDeserializer : RawDeserializer
    {
        private Environment currentEnvironment;

        public override string RawPath => "Environments";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Environments");
            XmlNodeList environmentNodes = root.SelectNodes("Environment");
            foreach (XmlNode environmentNode in environmentNodes)
                DeserializeEnvironment(environmentNode);
        }

        private void DeserializeEnvironment(XmlNode node)
        {
            currentEnvironment = new Environment();
            currentEnvironment.name = EvaluateAttribute(node, "Name", true);
            CultureInfo info = CultureInfo.InvariantCulture;
            currentEnvironment.furnishingDensity = float.Parse(EvaluateNode(node, "FurnishingDensity", true), info);
            currentEnvironment.furnishers = EvaluateFurnishers(node.SelectSingleNode("Furnishers"));
            EvaluateFurnishings(node.SelectSingleNode("Furnishings"));
            Assets.Add(currentEnvironment.name, currentEnvironment);
        
        }

        private List<Furnisher> EvaluateFurnishers(XmlNode root)
        {
            List<Furnisher> furnishers = new List<Furnisher>();
            XmlNodeList furnisherNodes = root.SelectNodes("Furnisher");
            foreach (XmlNode furnisherNode in furnisherNodes)
            {
                Furnisher furnisher = DeserializeFurnisher(furnisherNode);
                furnisher.environment = currentEnvironment;
                furnishers.Add(furnisher);
            }
            return furnishers;
        }

        private Furnisher DeserializeFurnisher(XmlNode node)
        {
            string name = EvaluateAttribute(node, "Name", true);
            Furnisher furnisher = InstantiateFurnisher(name);
            CultureInfo info = CultureInfo.InvariantCulture;
            furnisher.weight = float.Parse(EvaluateAttribute(node, "Weight") ?? "1", info);
            furnisher.forced = bool.Parse(EvaluateAttribute(node, "Forced") ?? "False");
            furnisher.exclusive = bool.Parse(EvaluateAttribute(node, "Exclusive") ?? "False");
            return furnisher;
        }

        private Furnisher InstantiateFurnisher(string furnisherName)
        {
            Type type = TypeResolver.ResolveType("TCD.Zones.Environments." + furnisherName);
            return (Furnisher) Activator.CreateInstance(type);
        }

        private void EvaluateFurnishings(XmlNode root)
        {
            XmlNodeList furnishingNodes = root.SelectNodes("Furnishing");
            foreach (XmlNode furnishingNode in furnishingNodes)
                EvaluateFurnishing(furnishingNode);
        }

        private void EvaluateFurnishing(XmlNode node)
        {
            Furnishing furnishing = new Furnishing();
            furnishing.name = EvaluateAttribute(node, "Name", true);
            furnishing.weight = int.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            string unsplitCategories = EvaluateAttribute(node, "Category", true);
            string[] categories = unsplitCategories.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string category in categories)
                currentEnvironment.AddFurnishing(category, furnishing);
        }
    }
}
