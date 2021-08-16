using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        protected AudioSource AudioSource
        {
            get
            {
                if (!audioSource)
                    audioSource = GetComponentInChildren<AudioSource>();
                return audioSource;
            }
        }

        public void Play(AudioClip audioClip)
        {
            if (!audioClip)
                return;
            AudioSource.PlayOneShot(audioClip);
        }
    }
}
