using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Hearing : Part
    {
        [SerializeField] private float sensitivity;
        
        private Noise lastHeardDistractingNoise;

        public override string Name => "Hearing";

        public float Sensitivity
        {
            get => sensitivity;
            set => sensitivity = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<BeforeTurnTickEvent>(this, OnBeforeTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<BeforeTurnTickEvent>(this);
        }

        private void OnBeforeTurnTick(BeforeTurnTickEvent e)
        {
            if (e.timeElapsed > 0)
                lastHeardDistractingNoise = default;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == AICommandEvent.id)
                OnAICommand((AICommandEvent)(LocalEvent) e);
            return base.HandleEvent(e);
        }

        private void OnAICommand(AICommandEvent e)
        {
            if (!e.hasActed && HearsDistractingNoise())
            {

            }
        }

        private bool HearsDistractingNoise()
        {
            return lastHeardDistractingNoise.type >= NoiseType.Distracting;
        }

        public bool CanHear()
        {
            return true;
        }

        public void HearNoise(Noise noise)
        {
            if (CanHear() && noise.CompareTo(lastHeardDistractingNoise) > 0)
                lastHeardDistractingNoise = noise;
        }
    }
}
