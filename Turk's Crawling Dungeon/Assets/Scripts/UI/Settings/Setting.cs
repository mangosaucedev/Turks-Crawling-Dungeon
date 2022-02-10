using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Inputs;
using TCD.Settings;

namespace TCD.UI
{
    public abstract class Setting : MonoBehaviour
    {
        [SerializeField] protected string settingName;
        protected object value;

        public virtual void Start()
        {
            LoadSetting();
        }

        private void OnEnable()
        {
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            EventManager.StopListening<KeyEvent>(this);
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.state != KeyState.PressedThisFrame)
                return;
            if (e.context.command == KeyCommand.Reset)
                LoadSetting();
        }

        public abstract void LoadSetting();

        public abstract void UpdateSetting();

        public void SaveSetting() =>
            SettingsManager.Set(settingName, value.ToString());
    }
}
