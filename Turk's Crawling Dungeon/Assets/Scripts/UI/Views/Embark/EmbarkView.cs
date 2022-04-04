using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.UI;
using TCD.Zones.Dungeons;

namespace TCD
{
    public class EmbarkView : MonoBehaviour
    {
        public GameObject[] tabs;

        private void Start()
        {
            Embark.Reset();
        }

        public void OpenTab(GameObject tab)
        {
            foreach (GameObject gameObject in tabs)
                gameObject.SetActive(false);
            tab.SetActive(true);
        }

        public void EmbarkCampaign()
        {
            ViewManager.Close("Main Menu");
            ViewManager.Close("Embark View");
            CampaignHandler.StartCampaign(Embark.ChosenCampaign);
            PlayerInfo.currentPlayer.gameObject.SetActive(true);
        }    
    }
}
