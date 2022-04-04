using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;
using TCD.Settings;

namespace TCD.UI
{
    public class SettingsView : ViewController
    {
        private Setting[] settings;

        protected override string ViewName => "Settings View";

        private void Start()
        {
            settings = GetComponentsInChildren<Setting>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<KeyEvent>(this, OnKey);
            View.CloseEvent += SaveSettings;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<KeyEvent>(this);
            View.CloseEvent -= SaveSettings;
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.state != KeyState.PressedThisFrame)
                return;
            if (e.context.command == KeyCommand.ResetToDefaults)
                ResetSettingsToDefault();
        }

        private void ResetSettingsToDefault()
        {
            SettingsFile.ResetDefaults();
            SettingsManager manager = ServiceLocator.Get<SettingsManager>();
            manager.currentSettings = SettingsFile.defaultSettings;
            foreach (Setting setting in settings)
                setting.LoadSetting();
        }

        private void SaveSettings()
        {
            SettingsManager manager = ServiceLocator.Get<SettingsManager>();
            foreach (Setting setting in settings)
                setting.SaveSetting();
            manager.SaveSettings();
        }
    }
}
