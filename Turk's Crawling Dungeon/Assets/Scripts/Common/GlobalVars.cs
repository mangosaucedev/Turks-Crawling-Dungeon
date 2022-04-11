using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    [ContainsGameStatics]
    public static class GlobalVars 
    {
        [GameStatic(null)] private static List<GlobalVar> allGlobalVars = new List<GlobalVar>();
        [GameStatic(null)] private static Dictionary<string, GlobalVar> globalVarsByName = new Dictionary<string, GlobalVar>();

        private static List<GlobalVar> AllGlobalVars
        {
            get
            {
                if (allGlobalVars == null)
                    allGlobalVars = new List<GlobalVar>();
                return allGlobalVars;
            }
        }

        private static Dictionary<string, GlobalVar> GlobalVarsByName
        {
            get
            {
                if (globalVarsByName == null)
                    globalVarsByName = new Dictionary<string, GlobalVar>();
                return globalVarsByName;
            }
        }

        public static void Set(string name, object value)
        {
            GlobalVar var = Find(name);
            var.value = value;
        }

        private static GlobalVar Find(string name)
        {
            if (!GlobalVarsByName.TryGetValue(name, out GlobalVar var))
            {
                var = new GlobalVar(name, false);
                AllGlobalVars.Add(var);
                GlobalVarsByName[name] = var;
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

        public static GlobalVar[] GetArray() => AllGlobalVars.ToArray();
    }
}
