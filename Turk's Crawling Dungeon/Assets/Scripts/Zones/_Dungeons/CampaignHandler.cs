using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Dungeons
{
    [ContainsGameStatics]
    public static class CampaignHandler 
    {
        [GameStatic(null)] public static Campaign currentCampaign;
        [GameStatic(0)] public static int currentDungeonIndex;

        public static void StartCampaign(string campaignName)
        {
            Campaign campaign = Assets.Get<Campaign>(campaignName);
            StartCampaign(campaign);
        }

        public static void StartCampaign(Campaign campaign)
        {
            currentCampaign = campaign;
            currentDungeonIndex = 0;
            DungeonHandler.GoToDungeon(campaign.Dungeons[0]);
        }

        public static void GoToNextDungeon()
        {
            currentDungeonIndex++;
            if (currentDungeonIndex < currentCampaign.Dungeons.Count)
                DungeonHandler.GoToDungeon(currentCampaign.Dungeons[currentDungeonIndex]);
            else
                TCDGame.WinGame();
        }
    }
}
