using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace TCD
{
    public static class VersionInfo 
    {
        private static string versionName;

        public static string GetVersionName()
        {
            if (string.IsNullOrEmpty(versionName))
                versionName = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return versionName;
        }
    }
}
