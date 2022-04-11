using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.UI;
using TCD.Zones;
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
            ViewManager.Close("Prerelease Embark View");
            Instantiate(Assets.Get<GameObject>("Over Screen Fade"), ParentManager.OverScreen);
            if (Embark.ChosenCampaign != null)
                CampaignHandler.StartCampaign(Embark.ChosenCampaign);
            else
                ZoneResetter.ResetZone(false);
            PlayerInfo.currentPlayer.gameObject.SetActive(true);
            PlayerUtility.ApplyEmbarkProfile();
        }    
    }
}
