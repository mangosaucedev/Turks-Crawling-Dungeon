using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs.Hotbar
{
    public static class Hotbar
    {
        public static List<HotbarAction> actions = new List<HotbarAction>();
        
        private static KeyCommand[] hotbarCommands;

        static Hotbar()
        {
            hotbarCommands = new KeyCommand[]
            {
                KeyCommand.Hotbar1,
                KeyCommand.Hotbar2,
                KeyCommand.Hotbar3,
                KeyCommand.Hotbar4,
                KeyCommand.Hotbar5,
                KeyCommand.Hotbar6,
                KeyCommand.Hotbar7,
                KeyCommand.Hotbar8,
                KeyCommand.Hotbar9,
                KeyCommand.Hotbar0,
                KeyCommand.HotbarS1,
                KeyCommand.HotbarS2,
                KeyCommand.HotbarS3,
                KeyCommand.HotbarS4,
                KeyCommand.HotbarS5,
                KeyCommand.HotbarS6,
                KeyCommand.HotbarS7,
                KeyCommand.HotbarS8,
                KeyCommand.HotbarS9,
                KeyCommand.HotbarS0,
                KeyCommand.HotbarC1,
                KeyCommand.HotbarC2,
                KeyCommand.HotbarC3,
                KeyCommand.HotbarC4,
                KeyCommand.HotbarC5,
                KeyCommand.HotbarC6,
                KeyCommand.HotbarC7,
                KeyCommand.HotbarC8,
                KeyCommand.HotbarC9,
                KeyCommand.HotbarC0,
            };
        }

        public static void AddAction(HotbarAction action, int index)
        {

        }

        public static void RemoveAction(HotbarAction action)
        {

        }

        public static void RemoveAction(int index)
        {

        }
    }
}
