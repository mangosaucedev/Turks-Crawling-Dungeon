using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Settings;

namespace TCD
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource ambienceSource;
        [SerializeField] private AudioSource musicSource;

        protected AudioSource SfxSource
        {
            get
            {
                if (!sfxSource)
                {
                    Transform transform = this.transform.Find("Sfx Source");
                    sfxSource = transform.GetComponent<AudioSource>();
                }
                return sfxSource;
            }
        }

        protected AudioSource AmbienceSource
        {
            get
            {
                if (!ambienceSource)
                {
                    Transform transform = this.transform.Find("Ambience Source");
                    ambienceSource = transform.GetComponent<AudioSource>();
                }
                return ambienceSource;
            }
        }

        protected AudioSource MusicSource
        {
            get
            {
                if (!musicSource)
                {
                    Transform transform = this.transform.Find("Music Source");
                    musicSource = transform.GetComponent<AudioSource>();
                }
                return musicSource;
            }
        }

        private void OnEnable()
        {
            UpdateSettings();
            EventManager.Listen<SettingChangedEvent>(this, OnSettingChanged);
        }

        private void OnDisable()
        {
            EventManager.StopListening<SettingChangedEvent>(this);
        }

        private void UpdateSettings()
        {
            SfxSource.volume = SettingsManager.Get<float>("SfxVolume");
            SfxSource.mute = SettingsManager.Get<bool>("SfxMuted");
            AmbienceSource.volume = SettingsManager.Get<float>("AmbienceVolume");
            AmbienceSource.mute = SettingsManager.Get<bool>("AmbienceMuted");
            MusicSource.volume = SettingsManager.Get<float>("MusicVolume");
            MusicSource.mute = SettingsManager.Get<bool>("MusicMuted");
            if (SettingsManager.Get<bool>("MasterMuted"))
            {
                SfxSource.mute = true;
                AmbienceSource.mute = true;
                MusicSource.mute = true;
            }
        }

        private void OnSettingChanged(SettingChangedEvent e) => UpdateSettings();

        public void PlayOneShot(AudioClip audioClip)
        {
            if (!audioClip)
                return;
            SfxSource.PlayOneShot(audioClip);
        }
    }
}
