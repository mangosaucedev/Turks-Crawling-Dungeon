using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    [Serializable]
    public class ObjectBlueprint
    {
        public string name;
        public string displayName;
        public string displayNamePlural;
        public string description;
        public string faction;
        public string size;
        public string hpMax;
        public string hp;
        public string value;
        public string inheritsFrom;

        public Dictionary<string, PartBlueprint> partsByName = 
            new Dictionary<string, PartBlueprint>();
        public List<PartBlueprint> parts = new List<PartBlueprint>();
        public List<string> partNames = new List<string>();

        public Dictionary<string, PartBlueprint> overridenPartsByName =
            new Dictionary<string, PartBlueprint>();
        public List<PartBlueprint> overridenParts = new List<PartBlueprint>();
        public List<string> overridenPartNames = new List<string>();

        public List<string> removedParts = new List<string>();

        public void AddPart(PartBlueprint part)
        {
            if (part == null || part?.name == null || partsByName.ContainsKey(part.name) || 
                parts.Contains(part))
                return;
            partsByName.Add(part.name, part);
            parts.Add(part);
            partNames.Add(part.name);
        }

        public PartBlueprint GetPart(string partName)
        {
            if (partsByName.TryGetValue(partName, out PartBlueprint part))
                return part;
            return null;
        }

        public void AddOverridePart(PartBlueprint part)
        {
            if (part == null || part?.name == null || overridenPartsByName.ContainsKey(part.name) ||
                overridenParts.Contains(part))
                return;
            overridenPartsByName.Add(part.name, part);
            overridenParts.Add(part);
            overridenPartNames.Add(part.name);
        }

        public void AddRemovePart(string partName)
        {
            if (!string.IsNullOrEmpty(partName) && !removedParts.Contains(partName))
                removedParts.Add(partName);
        }
        
    }
}
