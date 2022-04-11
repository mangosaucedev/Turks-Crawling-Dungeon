using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.UI
{
    public class EmbarkSummary : MonoBehaviour
    {
        private const string ENDLESS_DESCRIPTION = 
            "Endless dungeon mode allows players to navigate a never-ending series of hostile " +
            "zones in a freeform quest to get the highest score possible.";

        [SerializeField] private Text campaign;
        [SerializeField] private Text campaignDescription;
        [SerializeField] private Text className;
        [SerializeField] private Text classDescription;
        [SerializeField] private Text talents;
        [SerializeField] private Text resources;

        private void OnEnable()
        {
            campaign.text = "Campaign: " + Embark.ChosenCampaign?.name ?? "Endless Dungeon";
            campaignDescription.text = "\t" + Embark.ChosenCampaign?.description ?? ENDLESS_DESCRIPTION;
            className.text = "Class: " + Embark.ChosenClass.name;
            classDescription.text = "\t" + Embark.ChosenClass.description;
            talents.text = "Talents: ";
            HashSet<string> talentNames = Embark.ChosenTalents;
            int i = 0;
            foreach (string talentName in talentNames)
            {
                int level = Embark.ChosenTalentLevels[talentName];
                talents.text += talentName + " (" + level + ") ";
                if (i < talentNames.Count - 1)
                    talents.text += ", ";
                i++;
            }                
            resources.text = "Resources:";

            Resources playerResources = PlayerInfo.currentPlayer.Parts.Get<Resources>();
            foreach (Resource resource in Enum.GetValues(typeof(Resource)))
                resources.text += "\n\t " + resource + ": " + playerResources.GetMaxResource(resource);
        }
    }
}
