using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TCD
{
    public static class DebugCrashHandler 
    {
        private static string CrashFilePath => Application.persistentDataPath + "/..txt";

        public static void CreateCrashFile()
        {
            if (!CrashFilePersists())
                File.WriteAllText(CrashFilePath, string.Empty);
        }

        public static void DeleteCrashFile()
        {
            if (CrashFilePersists())
                File.Delete(CrashFilePath);
        }

        public static bool CrashFilePersists() => File.Exists(CrashFilePath);
    }
}
