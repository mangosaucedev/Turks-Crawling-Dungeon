using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public class CinematicManager : MonoBehaviour
    {
        public Cinematic currentCinematic;
        
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
        }

        public void StopCinematic()
        {
            StopAllCoroutines();
            currentCinematic = null;
        }

        private IEnumerator PlayCinematicRoutine()
        {
            yield return null;
        }
    }
}
