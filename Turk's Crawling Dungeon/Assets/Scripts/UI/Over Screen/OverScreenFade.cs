using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class OverScreenFade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void OnEnable()
        {
            StartCoroutine(FadeRoutine(0f, 0.25f, 0.5f));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public IEnumerator FadeRoutine(float alpha, float holdTime, float fadeTime)
        {
            yield return new WaitForSeconds(holdTime);
            if (fadeTime <= 0f)
            {
                canvasGroup.alpha = alpha;
                yield break;
            }
            float fade = 0f;
            float startAlpha = canvasGroup.alpha;
            while (fade < 1f)
            {
                fade = Mathf.Min(fade + (float)(Time.deltaTime / fadeTime), 1f);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, alpha, fade);
                yield return null;
            }
            if (canvasGroup.alpha == 0f)
                Destroy(gameObject);
        }
    }
}
