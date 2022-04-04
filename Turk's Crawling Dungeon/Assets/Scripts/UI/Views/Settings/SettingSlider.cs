using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Settings;

namespace TCD.UI
{
    public class SettingSlider : Setting
    {
        [SerializeField] private Slider slider;

        public override void Start()
        {
            base.Start();
            slider.onValueChanged.AddListener(value => UpdateSetting());
        }

        public override void LoadSetting()
        {
            float value = SettingsManager.Get<float>(settingName);
            this.value = value; 
            slider.value = value;
        }

        public override void UpdateSetting()
        {
            value = slider.value;
        }
    }
}
