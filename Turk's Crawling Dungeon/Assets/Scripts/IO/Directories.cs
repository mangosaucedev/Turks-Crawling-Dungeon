using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TCD.IO
{
    public static class Directories 
    {

        #region Streaming (Read-Only)

        public static string Raws => GetDirectory(Application.streamingAssetsPath + "/Raws/");

        public static string Dialogues => GetDirectory(Raws + "Dialogues");

        public static string Speakers => GetDirectory(Raws + "Speakers");

        public static string Factions => GetDirectory(Raws + "Factions");

        #endregion

        private static string GetDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
