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
            currentZoneName = EvaluateAttribute(node, "Name", true);
            XmlNode paramsNode = node.SelectSingleNode("Params");
            DeserializeParams(paramsNode);
            XmlNode environmentsNode = node.SelectSingleNode("Environments");
            DeserializeEnvironments(environmentsNode);
            XmlNode terrainNode = node.SelectSingleNode("Terrain");
            DeserializeTerrain(terrainNode);
            XmlNode encountersNode = node.SelectSingleNode("Encounters");
            DeserializeEncounters(encountersNode);
        }

        private void DeserializeParams(XmlNode node)
        {
            IZoneParams zoneParams = new ZoneParams();
            zoneParams.Width = int.Parse(EvaluateNode(node, "Width", true));
            zoneParams.Height = int.Parse(EvaluateNode(node, "Height", true));
            zoneParams.MinChamberWidth = int.Parse(EvaluateNode(node, "MinChamberWidth", true));
            zoneParams.MinChamberHeight = int.Parse(EvaluateNode(node, "MinChamberHeight", true));
            zoneParams.MaxChamberWidth = int.Parse(EvaluateNode(node, "MaxChamberWidth", true));
            zoneParams.MaxChamberHeight = int.Parse(EvaluateNode(node, "MaxChamberHeight", true));
            zoneParams.MaxChambers = int.Parse(EvaluateNode(node, "MaxChambers", true));
            zoneParams.MinChamberChildDistance = int.Parse(EvaluateNode(node, "MinChamberChildDistance", true));
            zoneParams.MinChamberBufferDistance = int.Parse(EvaluateNode(node, "MinChamberBufferDistance", true));
            zoneParams.MinCorridorWidth = int.Parse(EvaluateNode(node, "MinCorridorWidth", true));
            zoneParams.MaxCorridorWidth = int.Parse(EvaluateNode(node, "MaxCorridorWidth", true));
            zoneParams.GenerateCaves = bool.Parse(EvaluateNode(node, "GenerateCaves", true));
            zoneParams.CavePenetrationChance = int.Parse(EvaluateNode(node, "CavePenetrationChance", true));
            Assets.Add(currentZoneName, zoneParams);
        }

        private void DeserializeEnvironments(XmlNode node)
        {
            currentZoneEnvironments = new ZoneEnvironments();
            XmlNodeList environmentNodes = node.SelectNodes("Environment");
            foreach (XmlNode environmentNode in environmentNodes)
                DeserializeEnvironment(environmentNode);
            Assets.Add(currentZoneName, currentZoneEnvironments);
        }

        private void DeserializeEnvironment(XmlNode node)
        {
            string name = EvaluateAttribute(node, "Name", true);
            float weight = float.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            EnvironmentPlacement placement = 
                (EnvironmentPlacement) Enum.Parse(typeof(EnvironmentPlacement), EvaluateAttribute(node, "Placement") ?? "Random");
            bool exclusive = bool.Parse(EvaluateAttribute(node, "Exclusive") ?? "False");
            bool forced = bool.Parse(EvaluateAttribute(node, "Forced") ?? "False");
            currentZoneEnvironments.AddEnvironmentReference(name, weight, placement, exclusive, forced);
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

        private void DeserializeEncounters(XmlNode node)
        {
            currentZoneEncounters = new ZoneEncounters();
            currentZoneEncounters.density = float.Parse(EvaluateAttribute(node, "Density") ?? "1");
            XmlNodeList encounterNodes = node.SelectNodes("Encounter");
            foreach (XmlNode encounterNode in encounterNodes)
                DeserializeEncounter(encounterNode);
            Assets.Add(currentZoneName, currentZoneEncounters);
        }

        private void DeserializeEncounter(XmlNode node)
        {
            ZoneEncounter encounter = new ZoneEncounter();
            encounter.name = EvaluateAttribute(node, "Name", true);
            encounter.weight = float.Parse(EvaluateAttribute(node, "Weight") ?? "1");
            string type = EvaluateAttribute(node, "Type") ?? "Random";
            encounter.type = (EncounterType) Enum.Parse(typeof(EncounterType), type);
            encounter.forced = bool.Parse(EvaluateAttribute(node, "Forced") ?? "False");
            encounter.exclusive = bool.Parse(EvaluateAttribute(node, "Exclusive") ?? "False");
            currentZoneEncounters.buildEncounters.Add(encounter);
        }
    }
}
