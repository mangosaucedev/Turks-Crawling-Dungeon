using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TCD.UI.Notifications;

namespace TCD.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public SettingsFile currentSettings;

        private bool failedToLoadInvalidFile;
        private string invalidFileNotification;
        private bool showedNotification;

        private static string SettingsFilePath => Application.persistentDataPath + "/Settings.txt";

        private void Awake()
        {
            currentSettings = SettingsFile.defaultSettings;
            LoadSettings();
        }

        private void OnEnable()
        {
            if (!showedNotification)
                EventManager.Listen<ViewOpenedEvent>(this, OnViewOpened);
        }

        private void OnDisable()
        {
            if (!showedNotification)
                EventManager.StopListening<ViewOpenedEvent>(this);
        }

        private void OnViewOpened(ViewOpenedEvent e)
        {
            if (e.view.name == "Main Menu" && failedToLoadInvalidFile && !showedNotification)
            {
                showedNotification = true;
                NotificationHandler.Notify("Failed To Load Player Settings!", invalidFileNotification);
                EventManager.StopListening<ViewOpenedEvent>(this);
            }
        }


        private void EnsureSettingsFileExists()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                    File.WriteAllText(SettingsFilePath, SettingsFile.defaultSettings.ToJson());
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(
                    new IOException("Failed to ensure Settings file exists @" + SettingsFilePath + ": " + e.Message));
            }
        }


        public void LoadSettings()
        {
            EnsureSettingsFileExists();
            string json = File.ReadAllText(SettingsFilePath);
            try
            {
                GetCurrentSettingsFromJson(json);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(
                    new Exception("Failed to load Settings from file @" + SettingsFilePath + ": " + e.Message));
            }
        }

        private void GetCurrentSettingsFromJson(string json)
        {
            currentSettings = JsonUtility.FromJson<SettingsFile>(json);
            if (currentSettings.entries.Count == 0)
            {
                DebugLogger.LogError("Failed to load settings: no entries found in file!");
                invalidFileNotification = "Player settings file @" + SettingsFilePath + " contains no entries " +
                    "and could not be loaded. File will be rewritten with default values.";
            }    
            else if (currentSettings.version != VersionInfo.GetVersionName() || 
                currentSettings.entries.Count != SettingsFile.defaultSettings.entries.Count ||
                currentSettings.entries[0].name != SettingsFile.defaultSettings.entries[0].name)
            {
                DebugLogger.LogError("Settings file out-of-date! Rewriting...");
                invalidFileNotification = "Player settings file @" + SettingsFilePath + " is from a different game version " +
                    "and could not be loaded. File will be rewritten with default values.";
            }
            if (!currentSettings.IsValid())
            {
                failedToLoadInvalidFile = true;
                File.WriteAllText(SettingsFilePath, SettingsFile.defaultSettings.ToJson());
                currentSettings = SettingsFile.defaultSettings;
                return;
            }
            DebugLogger.Log("Settings successfully loaded from file @" + SettingsFilePath);
        }

        public void SaveSettings()
        {
            EnsureSettingsFileExists();
            string json = currentSettings.ToJson();
            try
            {
                using (StreamWriter writer = new StreamWriter(SettingsFilePath))
                    writer.WriteLine(json);
                DebugLogger.Log("Settings successfully saved to file @" + SettingsFilePath);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(
                    new IOException("Failed to save settings to file @" + SettingsFilePath + ":" + e.Message));
            }    
        }

        public static T Get<T>(string entryName)
        {
            SettingsManager manager = ServiceLocator.Get<SettingsManager>();
            return manager.GetValue<T>(entryName);
        }

        public T GetValue<T>(string entryName) =>
            currentSettings.GetEntry<T>(entryName);

        public static object Get(string entryName) => 
            Get<object>(entryName);

        public static void Set(string entryName, string value)
        {
            SettingsManager manager = ServiceLocator.Get<SettingsManager>();
            manager.SetValue(entryName, value);
        }

        public void SetValue(string entryName, string value) =>
            currentSettings.SetEntry(entryName, value);
        
    }
}
