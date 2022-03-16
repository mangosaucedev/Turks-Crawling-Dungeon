using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public static class GlobalVars 
    {
        private static List<GlobalVar> globalVars = new List<GlobalVar>();
        private static Dictionary<string, GlobalVar> globalVarsByName = new Dictionary<string, GlobalVar>();

        public static void Set(string name, object value)
        {
            GlobalVar var = Find(name);
            var.value = value;
        }

        private static GlobalVar Find(string name)
        {
            if (!globalVarsByName.TryGetValue(name, out GlobalVar var))
            {
                var = new GlobalVar(name, false);
                globalVars.Add(var);
                globalVarsByName[name] = var;
            }
            return var;
        }

        public static object Get(string name)
        {
            GlobalVar var = Find(name);
            return var.value;
        }

        public static bool TryGet(string name, out object value)
        {
            value = Get(name);
            return value != null;
        }

        public static GlobalVar[] GetArray() => globalVars.ToArray();
    }
}
