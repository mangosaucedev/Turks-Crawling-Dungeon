using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.TimeManagement;

namespace TCD.Cheats
{
    [ContainsConsoleCommand]      
    public static class InventoryCheats 
    {
        [ConsoleCommand("fill_player_inventory")]
        [ConsoleCommand("fpi")]
        public static void FillPlayerInventory()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            if (!player || !player.Parts.TryGet(out Inventory inventory))
                return;
            for (int i = 0; i < 100; i++)
            {
                BaseObject item = ObjectFactory.BuildFromBlueprint("JadePsyscourge", new Vector2Int(0, 0));
                ActionScheduler.EnqueueAction(item, () => { inventory.AddItem(item); });
                TimeScheduler.Tick(0);
            }
        }

        [ConsoleCommand("fill_player_inventory_page")]
        [ConsoleCommand("fpip")]
        public static void FillPlayerInventoryPage()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            if (!player || !player.Parts.TryGet(out Inventory inventory))
                return;
            for (int i = 0; i < 16; i++)
            {
                BaseObject item = ObjectFactory.BuildFromBlueprint("JadePsyscourge", new Vector2Int(0, 0));
                ActionScheduler.EnqueueAction(item, () => { inventory.AddItem(item); });
                TimeScheduler.Tick(0);
            }
        }
    }
}
