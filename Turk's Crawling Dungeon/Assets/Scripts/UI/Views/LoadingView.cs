using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class LoadingView : MonoBehaviour
    {
        private const float DISPLAY_SCREEN_FOR_SECONDS = 2f;
        private const float FADE_TIME = 0.35f;
        private const float DARK_FOR_SECONDS = 0.35f;

        [SerializeField] private Text message;
        [SerializeField] private Image screen;
        [SerializeField] private Image bar;
        [SerializeField] private Sprite[] screens;
        [SerializeField] private Color fullColor;

        private LoadingManager loadingManager;
        private int screenIndex;
        private WaitForSeconds displayScreenWait = new WaitForSeconds(DISPLAY_SCREEN_FOR_SECONDS);
        private WaitForSeconds darkWait = new WaitForSeconds(DARK_FOR_SECONDS);

        private void Awake()
        {
            loadingManager = ServiceLocator.Get<LoadingManager>();
        }

        private void OnEnable()
        {
            int screenCount = screens.Length;
            screenIndex = Random.Range(0, screenCount);
            screen.sprite = screens[screenIndex];
            if (screenCount > 1)
                StartCoroutine(TransitionScreensRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            ILoadingOperation operation = loadingManager.CurrentLoadingOperation;
            if (operation != null)
            {
                message.text = operation.Message;
                bar.fillAmount = operation.Progress;
            }
            else
                message.text = "...";
        }

        private IEnumerator TransitionScreensRoutine()
        {
            int screenCount = screens.Length;
            while (true)
            {
                yield return displayScreenWait;
                yield return FadeRoutine(Color.black.With(a: (float) 64f / 255f));
                yield return darkWait;
                bool lastImage = screenIndex == screenCount - 1;
                screenIndex =  lastImage ? 0 : screenIndex + 1;
                screen.sprite = screens[screenIndex];
                yield return FadeRoutine(fullColor);
            }
        }

        private IEnumerator FadeRoutine(Color targetColor)
        {
            float fade = 0f;
            Color startColor = screen.color;
            while (screen.color != targetColor)
            {
                screen.color = Color.Lerp(startColor, targetColor, fade);
                fade = Mathf.Min(fade + (float) Time.unscaledDeltaTime / FADE_TIME, 1f);
                yield return null;
            }
        }
    }
}
