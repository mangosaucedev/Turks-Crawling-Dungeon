using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class CinematicFadeColor : CinematicView
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image image;

        public IEnumerator FadeToColorRoutine(Color color, float alpha, float fadeTime)
        {
            if (fadeTime <= 0)
            {
                canvasGroup.alpha = alpha;
                image.color = color;
                yield break;
            }
            float fade = 0f;
            float startAlpha = canvasGroup.alpha;
            Color startColor = image.color;
            while (fade < 1f)
            {
                fade = Mathf.Min(fade + (float) (Time.deltaTime / fadeTime), 1f);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, alpha, fade);
                image.color = Color.Lerp(startColor, color, fade);
                yield return null;
            }
            if (canvasGroup.alpha == 0f)
                ViewManager.Close(name);
        }
    }
}
