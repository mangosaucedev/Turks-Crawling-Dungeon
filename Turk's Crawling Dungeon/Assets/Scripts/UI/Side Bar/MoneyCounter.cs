using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private Text moneyCounter;

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
                    Wallet playerWallet = player.Parts.Get<PlayerWallet>();
                    moneyCounter.text = "$" + playerWallet.money.ToString();
                }
                yield return wait;
            }
        }
    }
}
