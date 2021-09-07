using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects.Encounters;
using TCD.Zones;
using TCD.Zones.Environments;

namespace TCD.IO
{
    public class ZoneDeserializer : RawDeserializer
    {
        private Zone currentZone;
        private string currentZoneName;
        private ZoneEnvironments currentZoneEnvironments;
        private ZoneTerrain currentZoneTerrain;
        private ZoneEncounters currentZoneEncounters;

        public override string RawPath => "Zones";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            XmlNode root = xml.SelectSingleNode("Zones");
            XmlNodeList zoneNodes = root.SelectNodes("Zone");
            foreach (XmlNode zoneNode in zoneNodes)
                DeserializeZone(zoneNode);
        }

        private void DeserializeZone(XmlNode node)
        {
            currentZone = new Zone();
            currentZoneName = EvaluateAttribute(node, "Name", true);
            currentZone.name = currentZoneName;
            currentZone.zoneParamsName = EvaluateNode(node, "Params", true);
            XmlNode encountersNode = node.SelectSingleNode("Encounters");
            currentZone.zoneEncountersName = encountersNode.InnerText;
            currentZone.encounterDensity = float.Parse(EvaluateAttribute(encountersNode, "Density") ?? "1");
            currentZone.zoneEnvironmentsName = EvaluateNode(node, "Environments", true);
            Assets.Add(currentZoneName, currentZone);
            XmlNode terrainNode = node.SelectSingleNode("Terrain");
            DeserializeTerrain(terrainNode);
        }

        private void DeserializeTerrain(XmlNode node)   
        {
            currentZoneTerrain = new ZoneTerrain();
            XmlNode wallRoot = node.SelectSingleNode("Walls");
            XmlNodeList wallNodes = wallRoot.SelectNodes("Wall");
            foreach (XmlNode wallNode in wallNodes)
                DeserializeWall(wallNode);
            XmlNode floorRoot = node.SelectSingleNode("Floors");
            XmlNodeList floorNodes = floorRoot.SelectNodes("Floor");
            foreach (XmlNode floorNode in floorNodes)
                DeserializeFloor(floorNode);
            Assets.Add(currentZoneName, currentZoneTerrain);
        }

        private void DeserializeWall(XmlNode node)
        {
            Wall wall = new Wall();
            wall.name = EvaluateAttribute(node, "Name", true);
            wall.surface = int.Parse(EvaluateAttribute(node, "Surface") ?? "0");
            wall.weight = float.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            currentZoneTerrain.walls.Add(wall);
        }

        private void DeserializeFloor(XmlNode node)
        {
            Floor floor = new Floor();
            floor.name = EvaluateAttribute(node, "Name", true);
            floor.surface = int.Parse(EvaluateAttribute(node, "Surface") ?? "0");
            floor.weight = float.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            currentZoneTerrain.floors.Add(floor);
        }
    }
}
