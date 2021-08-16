using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class CarriedWeightCounter : MonoBehaviour
    {
        [SerializeField] private Text carryWeightCounter;

        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

        private void OnEnable()
        {
            StartCoroutine(UpdateCounter());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator UpdateCounter()
        {
            while (true)
            {
                BaseObject player = PlayerInfo.currentPlayer;
                if (player)
                {
                    Inventory playerInventory = player.parts.Get<Inventory>();
                    carryWeightCounter.text = "#" + playerInventory.GetWeight() + "/" + playerInventory.GetMaxWeight();
                }
                yield return wait;
            }
        }
    }
}
