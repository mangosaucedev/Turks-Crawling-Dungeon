using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.UI
{
    public class PlayerHitpointBar : MonoBehaviour
    {
        [SerializeField] private Image bar;

        private Resources playerResources;
        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

        private Resources PlayerResources
        {
            get
            {
                if (!playerResources)
                    playerResources = PlayerInfo.currentPlayer?.parts.Get<Resources>();
                return playerResources;
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
                    float hp = Mathf.Max(PlayerResources.GetResource(Resource.Hitpoints), 0);
                    float hpMax = Mathf.Max(PlayerResources.GetMaxResource(Resource.Hitpoints), 1);
                    float fill = hp / hpMax;
                    bar.fillAmount = fill;
                }
                yield return wait;
            }
        }
    }
}
