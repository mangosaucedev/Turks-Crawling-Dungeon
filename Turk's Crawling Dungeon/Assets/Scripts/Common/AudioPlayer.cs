using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            if (!audioSource)
                audioSource = GetComponentInChildren<AudioSource>();
        }

        public void Play(AudioClip audioClip)
        {
            if (!audioClip)
                return;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
