using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Zones.Dungeons;

namespace TCD
{
    public class EmbarkCampaignSelection : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private Text description;

        private List<Campaign> campaigns = new List<Campaign>();

        private void Start()
        {
            List<Campaign> campaigns = Assets.FindAll<Campaign>();
            foreach (Campaign campaign in campaigns)
            {
                if (campaign.hideSelection)
                    continue;
                this.campaigns.Add(campaign);
                dropdown.options.Add(new Dropdown.OptionData(campaign.name));
            }
        }

        public void OnCampaignSelected(int campaignIndex)
        {
            Campaign campaign = campaigns[campaignIndex];
            Embark.SetChosenCampaign(campaign);
            description.text = campaign.description;
            if (!nextButton.activeInHierarchy)
                nextButton.SetActive(true);
        }
    }
}
