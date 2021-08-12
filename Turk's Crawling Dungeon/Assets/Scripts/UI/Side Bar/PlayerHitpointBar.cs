using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class PlayerHitpointBar : MonoBehaviour
    {
        [SerializeField] private Image bar;

        private Hitpoints playerHitpoints;
        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

        private Hitpoints PlayerHitpoints
        {
            get
            {
                if (!playerHitpoints)
                    playerHitpoints = PlayerInfo.currentPlayer?.parts.Get<Hitpoints>();
                return playerHitpoints;
            }
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateBar());   
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator UpdateBar()
        {
            while (true)
            {
                if (PlayerInfo.currentPlayer)
                {
                    float hp = Mathf.Max(PlayerHitpoints.GetHp(), 0);
                    float hpMax = Mathf.Max(PlayerHitpoints.GetHpMax(), 1);
                    float fill = hp / hpMax;
                    bar.fillAmount = fill;
                }
                yield return wait;
            }
        }
    }
}
