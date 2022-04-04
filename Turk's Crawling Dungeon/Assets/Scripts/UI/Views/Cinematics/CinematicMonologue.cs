using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Texts;

namespace TCD.UI
{
    public class CinematicMonologue : CinematicView
    {
        private const float SECONDS_PER_CHARACTER = 0.05f;
        private const float SECONDS_PAUSE = 1.5f;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text monologue;

        private WaitForSeconds printWait = new WaitForSeconds(SECONDS_PER_CHARACTER);
        private WaitForSeconds pauseWait = new WaitForSeconds(SECONDS_PAUSE);

        public void StartMonologue(GameText text)
        {
            StopAllCoroutines();
            StartCoroutine(StartMonologueRoutine(text));
        }

        public IEnumerator StartMonologueRoutine(GameText text)
        {
            string str = text;
            for (int i = 0; i < str.Length; i++)
            {
                monologue.text = str.Substring(0, i + 1);
                yield return printWait;
            }
            yield return pauseWait;
        }

        public void ClearMonologue(float fadeTime)
        {
            StopAllCoroutines();
            StartCoroutine(ClearMonologueRoutine(fadeTime));
        }
        public IEnumerator ClearMonologueRoutine(float fadeTime)
        {
            if (fadeTime <= 0)
            {
                ViewManager.Close(name);
                yield break;
            }
            float fade = 0f;
            float startAlpha = canvasGroup.alpha;
            while (fade < 1f)
            {
                fade = Mathf.Min(fade + (float) (Time.deltaTime / fadeTime), 1f);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, fade);
                yield return null;
            }
            ViewManager.Close(name);
            DestroyImmediate(this);
        }
    }
}
