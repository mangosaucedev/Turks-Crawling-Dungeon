using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TCD.IO
{
    public static class Directories 
    {
        #region Persistent (Read/Write)

        public static string ModUtility => GetDirectory(Application.persistentDataPath + "/ModUtility/");
        
        public static string WritableObjects => GetDirectory(ModUtility + "Objects");

        #endregion

        #region Streaming (Read-Only)

        public static string Raws => GetDirectory(Application.streamingAssetsPath + "/Raws/");

        public static string Cinematics => GetDirectory(Raws + "Cinematics");

        public static string DamageTypes => GetDirectory(Raws + "DamageTypes");

        public static string Dialogues => GetDirectory(Raws + "Dialogues");

        public static string Speakers => GetDirectory(Raws + "Speakers");

        public static string Factions => GetDirectory(Raws + "Factions");

        public static string Objects => GetDirectory(Raws + "Objects_v2");

        #endregion

        private static string GetDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
