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
            dropdown.options.Add(new Dropdown.OptionData(" "));
            foreach (Campaign campaign in campaigns)
            {
                if (campaign.hideSelection)
                    continue;
                this.campaigns.Add(campaign);
                dropdown.options.Add(new Dropdown.OptionData(campaign.name));
            }
        }

        private void OnEnable()
        {
            dropdown.Select();
        }

        public void OnCampaignSelected(int campaignIndex)
        {
            campaignIndex -= 1;
            if (campaignIndex == -1)
            {
                if (nextButton.activeInHierarchy)
                    nextButton.SetActive(false);
                return;
            }
            Campaign campaign = campaigns[campaignIndex];
            Embark.SetChosenCampaign(campaign);
            description.text = campaign.description;
            if (!nextButton.activeInHierarchy)
                nextButton.SetActive(true);
        }
    }
}
