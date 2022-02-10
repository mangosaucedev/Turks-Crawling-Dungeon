using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Settings
{
    [Serializable]
    public class SettingsEntry 
    {
        public string name;
        public string value;
        public string type;

        public T GetValue<T>()
        {
            SettingsEntryType settingsType = (SettingsEntryType)Enum.Parse(typeof(SettingsEntryType), type);
            return (T) GetValueAsType(value, settingsType);
        }

        private object GetValueAsType(string value, SettingsEntryType type)
        {
            switch (type)
            {
                case SettingsEntryType.Bool:
                    return bool.Parse(value);
                case SettingsEntryType.Int:
                    return int.Parse(value);
                case SettingsEntryType.Float:
                    return float.Parse(value);
                default:
                    return value;
            }
        }

        public object GetValue() => GetValue<object>();
    }
}
