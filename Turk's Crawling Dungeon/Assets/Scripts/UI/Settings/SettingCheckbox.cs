using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Settings;

namespace TCD.UI
{
    public class SettingCheckbox : Setting
    {
        [SerializeField] private Toggle toggle;

        public override void Start()
        {
            base.Start();
            toggle.onValueChanged.AddListener(value => UpdateSetting());
        }

        public override void LoadSetting()
        {
            bool value = SettingsManager.Get<bool>(settingName);
            this.value = value;
            toggle.isOn = value;
        }

        public override void UpdateSetting()
        {
            value = toggle.isOn;
        }
    }
}
