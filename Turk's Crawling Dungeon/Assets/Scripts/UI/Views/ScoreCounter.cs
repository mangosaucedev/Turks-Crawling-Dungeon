using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text text;

        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);

        private void OnEnable()
        {
            StartCoroutine(ScoreUpdate());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator ScoreUpdate()
        {
            while (true)
            {
                text.text = ScoreHandler.GetScore().ToString();
                yield return wait;
            }
        }
    }
}
