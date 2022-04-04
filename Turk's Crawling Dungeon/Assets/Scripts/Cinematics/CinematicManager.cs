using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public class CinematicManager : MonoBehaviour
    {
        [HideInInspector] public Cinematic currentCinematic;
        
        private Coroutine cinematicCoroutine;

        private void OnEnable()
        {
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            StopCinematic();
            EventManager.StopListening<KeyEvent>(this);
        }

        private void OnKey(KeyEvent e)
        { 
            if (e.context.command == Inputs.KeyCommand.Cancel && e.context.state == Inputs.KeyState.PressedThisFrame)
                StopCinematic();
        }

        public void PlayCinematic(string name)
        {
            Cinematic cinematic = Assets.Get<Cinematic>(name);
            PlayCinematic(cinematic);
        }

        public void PlayCinematic(Cinematic cinematic)
        {
            if (currentCinematic != null)
                StopCinematic();
            currentCinematic = cinematic;
            if (currentCinematic != null)
                DebugLogger.Log("Cinematic " + currentCinematic.name + " queued for play.");
        }

        public void StopCinematic()
        {
            this.EnsureCoroutineStopped(ref cinematicCoroutine);
            if (currentCinematic != null)
                DebugLogger.Log("Cinematic " + currentCinematic.name + " stopping.");
            currentCinematic = null;
            EventManager.Send(new CinematicEndedEvent());
        }

        public void StartCinematic()
        {
            this.EnsureCoroutineStopped(ref cinematicCoroutine);
            if (currentCinematic != null)
            {
                DebugLogger.Log("Cinematic " + currentCinematic.name + " playing.");
                cinematicCoroutine = StartCoroutine(PlayCinematicRoutine());
                EventManager.Send(new CinematicStartedEvent());
            }
        }

        private IEnumerator PlayCinematicRoutine()
        {
            yield return currentCinematic.PlayRoutine();
            StopCinematic();
        }
    }
}
