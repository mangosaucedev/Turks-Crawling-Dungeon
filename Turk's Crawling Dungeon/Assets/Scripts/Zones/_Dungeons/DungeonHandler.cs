using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones.Dungeons
{
    [ContainsGameStatics]
    public static class DungeonHandler 
    {
        [GameStatic(null)] public static Dungeon currentDungeon;
        [GameStatic(0)] public static int currentZoneIndex;

        public static void GoToDungeon(string dungeonName, bool resetPlayer = false)
        {
            Dungeon dungeon = Assets.Get<Dungeon>(dungeonName);
            GoToDungeon(dungeon, resetPlayer);
        }

        public static void GoToDungeon(Dungeon dungeon, bool resetPlayer = false)
        {
            currentDungeon = dungeon;
            currentZoneIndex = 0;
            ZoneGeneratorManager zoneGenerator = ServiceLocator.Get<ZoneGeneratorManager>();
            zoneGenerator.GenerateZone(dungeon.Zones[0]);
        }

        public static void GoToNextZone()
        {
            currentZoneIndex++;
            if (currentZoneIndex < currentDungeon.Zones.Count)
            {
                ZoneGeneratorManager zoneGenerator = ServiceLocator.Get<ZoneGeneratorManager>();
                zoneGenerator.GenerateZone(currentDungeon.Zones[currentZoneIndex]);
            }
            else
                CampaignHandler.GoToNextDungeon();
        }
    }
}
