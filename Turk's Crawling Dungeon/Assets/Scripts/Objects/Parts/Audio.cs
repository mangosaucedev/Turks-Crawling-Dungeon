using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Audio : Part
    {
        [SerializeField] private string death;
        [SerializeField] private string pickedUp;
        [SerializeField] private string dropped;
        [SerializeField] private string doorOpened;
        [SerializeField] private string doorClosed;
        [SerializeField] private string hit0;
        [SerializeField] private string hit1;
        [SerializeField] private string hit2;
        [SerializeField] private string steppedOn0;
        [SerializeField] private string steppedOn1;
        [SerializeField] private string steppedOn2;

        private AudioPlayer audioPlayer;

        public string Death
        {
            get => death;
            set => death = value;
        }

        public string PickedUp
        {
            get => pickedUp;
            set => pickedUp = value;
        }

        public string Dropped
        {
            get => dropped;
            set => dropped = value;
        }

        public string DoorOpened
        {
            get => doorOpened;
            set => doorOpened = value;
        }
        
        public string DoorClosed
        {
            get => doorClosed;
            set => doorClosed = value;
        }

        public string Hit0
        {
            get => hit0;
            set => hit0 = value;
        }

        public string Hit1
        {
            get => hit1;
            set => hit1 = value;
        }

        public string Hit2
        {
            get => hit2;
            set => hit2 = value;
        }

        public string SteppedOn0
        {
            get => steppedOn0;
            set => steppedOn0 = value;
        }

        public string SteppedOn1
        {
            get => steppedOn1;
            set => steppedOn1 = value;
        }

        public string SteppedOn2
        {
            get => steppedOn2;
            set => steppedOn2 = value;
        }

        public override string Name => "Part";

        private AudioPlayer AudioPlayer
        {
            get
            {
                if (!audioPlayer)
                    audioPlayer = ServiceLocator.Get<AudioPlayer>();
                return audioPlayer;
            }
        }

        private AudioClip OnDeath => GetAudioClip(Death);

        private AudioClip OnPickedUp => GetAudioClip(pickedUp);

        private AudioClip OnDropped => GetAudioClip(Dropped);

        private AudioClip OnDoorOpened => GetAudioClip(DoorOpened);

        private AudioClip OnDoorClosed => GetAudioClip(DoorClosed);

        private AudioClip OnHit0 => GetAudioClip(Hit0);

        private AudioClip OnHit1 => GetAudioClip(Hit1);
        
        private AudioClip OnHit2 => GetAudioClip(Hit2);

        private AudioClip OnSteppedOn0 => GetAudioClip(SteppedOn0);

        private AudioClip OnSteppedOn1 => GetAudioClip(SteppedOn1);

        private AudioClip OnSteppedOn2 => GetAudioClip(SteppedOn2);

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == DestroyObjectEvent.id)
                PlayAudioClip(OnDeath);
            if (e.Id == DoorOpenedEvent.id)
                PlayAudioClip(OnDoorOpened);
            if (e.Id == DoorClosedEvent.id)
                PlayAudioClip(OnDoorClosed);
            if (e.Id == DroppedEvent.id)
                PlayAudioClip(OnDropped);
            if (e.Id == PickedUpEvent.id)
                PlayAudioClip(OnPickedUp);
            if (e.Id == AttackedEvent.id)
                PlayAudioClip(Choose.Random(new AudioClip[] { OnHit0, OnHit1, OnHit2 }));
            if (e.Id == EnteredCellEvent.id)
                PlayAudioClip(Choose.Random(new AudioClip[] { OnSteppedOn0, OnSteppedOn1, OnSteppedOn2 }));
            return base.HandleEvent(e);
        }

        private AudioClip GetAudioClip(string audioClipName) =>
             (string.IsNullOrEmpty(audioClipName)) ? null : Assets.Get<AudioClip>(audioClipName);

        public void PlayAudioClip(AudioClip audioClip)
        {
            if (!parent.parts.TryGet(out Visible visible) || visible.IsVisibleToPlayer())
                AudioPlayer.Play(audioClip);
        } 
    }
}
