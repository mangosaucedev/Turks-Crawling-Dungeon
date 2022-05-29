using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics.Dialogues;

namespace TCD.Cinematics
{
    public class CinematicManager : MonoBehaviour
    {
        private Queue<Cinematic> cinematics = new Queue<Cinematic>();
        
        public Cinematic CurrentCinematic
        {
            get
            {
                if (cinematics.Count == 0)
                    return null;
                return cinematics.Peek();
            }
        }
        
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
            if (e.context.command == Inputs.KeyCommand.Cancel 
                && e.context.state == Inputs.KeyState.PressedThisFrame
                && DialogueHandler.CurrentDialogue == null)
                StopCinematic();
        }

        public void PlayCinematic(string name)
        {
            Cinematic cinematic = Assets.Get<Cinematic>(name);
            PlayCinematic(cinematic);
        }

        public void PlayCinematic(Cinematic cinematic)
        {
            cinematics.Enqueue(cinematic);
            if (CurrentCinematic != null)
                DebugLogger.Log("Cinematic " + CurrentCinematic.name + " queued for play.");
        }

        public void StopCinematic()
        {
            this.EnsureCoroutineStopped(ref cinematicCoroutine);
            if (CurrentCinematic != null)
            {
                DebugLogger.Log("Cinematic " + CurrentCinematic.name + " stopping.");
                cinematics.Dequeue();
                EventManager.Send(new CinematicEndedEvent());
            }
            if (CurrentCinematic != null)
                StartCinematic();
        }

        private void SkipCinematic()
        {
            if (CurrentCinematic == null)
                return;
            CurrentCinematic.Skip();
        }

        public void StartCinematic()
        {
            this.EnsureCoroutineStopped(ref cinematicCoroutine);
            if (CurrentCinematic != null)
            {
                DebugLogger.Log("Cinematic " + CurrentCinematic.name + " playing.");
                cinematicCoroutine = StartCoroutine(PlayCinematicRoutine());
                EventManager.Send(new CinematicStartedEvent());
            }
        }

        private IEnumerator PlayCinematicRoutine()
        {
            yield return CurrentCinematic.PlayRoutine();
            StopCinematic();
        }

        public void FireEvent(string e) => EventManager.Send(new CinematicEventFiredEvent(e));
    }
}
