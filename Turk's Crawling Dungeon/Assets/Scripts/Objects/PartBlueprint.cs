using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects
{
    [Serializable]
    public class PartBlueprint
    {
        public string name;

        public Dictionary<string, object> variablesByName =
            new Dictionary<string, object>();
        public List<string> variables = new List<string>();

        private Type type;

        public Type GetPartType()
        {
            if (type == null)
                type = TypeResolver.ResolveType("TCD.Objects.Parts." + name);
            if (type == null)
                ExceptionHandler.Handle(new Exception("Could not resolve type " + name));
            return type;
        }

        public void AddVariable(string variableName, object value)
        {
            if (!variables.Contains(variableName))
            {
                variables.Add(variableName);
                variablesByName[variableName] = value;
            }
        }
    }
}
