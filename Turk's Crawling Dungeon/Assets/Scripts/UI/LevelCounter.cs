using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField] private Text text;

        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);

        private void OnEnable()
        {
            StartCoroutine(LevelUpdate());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator LevelUpdate()
        {
            while (true)
            {
                text.text = "lvl " + ScoreHandler.level.ToString();
                yield return wait;
            }
        }
    }
}
