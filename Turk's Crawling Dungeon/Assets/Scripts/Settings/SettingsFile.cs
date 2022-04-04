using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Settings
{
    [Serializable]
    public class SettingsFile
    {
        public static SettingsFile defaultSettings;

        public string version; 
        public List<SettingsEntry> entries = new List<SettingsEntry>();

        static SettingsFile()
        {
            ResetDefaults();
        }

        public static void ResetDefaults()
        {
            defaultSettings = new SettingsFile();
            defaultSettings.version = VersionInfo.GetVersionName();
            defaultSettings.AddEntry("SfxVolume", "0.5", SettingsEntryType.Float);
            defaultSettings.AddEntry("AmbienceVolume", "0.5", SettingsEntryType.Float);
            defaultSettings.AddEntry("MusicVolume", "0.5", SettingsEntryType.Float);
            defaultSettings.AddEntry("SfxMuted", "False", SettingsEntryType.Bool);
            defaultSettings.AddEntry("AmbienceMuted", "False", SettingsEntryType.Bool);
            defaultSettings.AddEntry("MusicMuted", "False", SettingsEntryType.Bool);
            defaultSettings.AddEntry("MasterMuted", "False", SettingsEntryType.Bool);
            defaultSettings.AddEntry("DeveloperMode", "False", SettingsEntryType.Bool);
        }

        public bool IsValid() =>
            entries.Count == defaultSettings.entries.Count && 
            (entries.Count > 0 && entries[0].name == defaultSettings.entries[0].name) &&
            version == VersionInfo.GetVersionName();

        public void AddEntry(string name, string value, SettingsEntryType type)
        {
            SettingsEntry entry = new SettingsEntry
            {
                name = name,
                value = value,
                type = type.ToString()
            };
            AddEntry(entry);
        }

        public void AddEntry(SettingsEntry entry) => 
            entries.Add(entry);
        

        public T GetEntry<T>(string name)
        {
            if (TryGetEntry(name, out var entry))
                return entry.GetValue<T>();
            return default;
        }

        public bool TryGetEntry(string name, out SettingsEntry entry)
        {
            entry = null;
            foreach (SettingsEntry e in entries)
            {
                if (e.name == name)
                {
                    entry = e;
                    return true;
                }
            }
            return false;
        }

        public void SetEntry(string name, string value)
        {
            if (TryGetEntry(name, out SettingsEntry entry))
            {       
                object previousValue = entry.GetValue();
                entry.value = value;
                EventManager.Send(SettingChangedEvent.FromPool(entry, previousValue));
            }
        }

        public string ToJson() => JsonUtility.ToJson(this, true);
    }
}
