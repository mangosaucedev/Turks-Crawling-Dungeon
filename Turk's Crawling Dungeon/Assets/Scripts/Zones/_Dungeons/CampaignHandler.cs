using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Dungeons
{
    [ContainsConsoleCommand, ContainsGameStatics]
    public static class CampaignHandler 
    {
        [GameStatic(null)] public static Campaign currentCampaign;
        [GameStatic(0)] public static int currentDungeonIndex;

        [ConsoleCommand("startcampaign")]
        [ConsoleCommand("sc")]
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

        [ConsoleCommand("nextdungeon")]
        public static void GoToNextDungeon()
        {
            currentDungeonIndex++;
            if (currentDungeonIndex < currentCampaign.Dungeons.Count)
                DungeonHandler.GoToDungeon(currentCampaign.Dungeons[currentDungeonIndex]);
            else
                TCDGame.WinGame();
        }

        [ConsoleCommand("resetcampaign")]
        [ConsoleCommand("rc")]
        public static void ResetCampaign()
        {
            currentCampaign = null;
            currentDungeonIndex = 0;
            ZoneResetter.ResetZone();
        }
    }
}
