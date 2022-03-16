using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public class LocalVarCollection 
    {
        private static List<LocalVar> localVars = new List<LocalVar>();
        private static Dictionary<string, LocalVar> localVarsByName = new Dictionary<string, LocalVar>();

        public void Set(string name, object value)
        {
            LocalVar var = Find(name);
            var.value = value;
        }

        private LocalVar Find(string name)
        {
            if (!localVarsByName.TryGetValue(name, out LocalVar var))
            {
                var = new LocalVar(name, false);
                localVars.Add(var);
                localVarsByName[name] = var;
            }
            return var;
        }

        public object Get(string name)
        {
            LocalVar var = Find(name);
            return var.value;
        }

        public bool TryGet(string name, out object value)
        {
            value = Get(name);
            return value != null;
        }

        public LocalVar[] GetArray() => localVars.ToArray();
    }
}
