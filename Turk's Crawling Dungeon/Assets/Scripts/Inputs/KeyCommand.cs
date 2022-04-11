using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public enum KeyCommand
    {
        None,
        // Movement
        MoveNorthwest,
        MoveNorth,
        MoveNortheast,
        MoveWest,
        MovePass,
        MoveEast,
        MoveSouthwest,
        MoveSouth,
        MoveSoutheast,
        //
        Enter,
        Cancel,
        ZoomIn,
        ZoomOut,
        // Actions
        Interact,
        InteractAdvanced,
        Rest,
        Look,
        Throw,
        // UI
        OpenInventory,
        OpenStatus,
        OpenHealth,
        OpenHelp,
        OpenTalents,
        // Hotbar
        Hotbar1,
        Hotbar2,
        Hotbar3,
        Hotbar4,
        Hotbar5,
        Hotbar6,
        Hotbar7,
        Hotbar8,
        Hotbar9,
        Hotbar0,
        HotbarS1,
        HotbarS2,
        HotbarS3,
        HotbarS4,
        HotbarS5,
        HotbarS6,
        HotbarS7,
        HotbarS8,
        HotbarS9,
        HotbarS0,
        HotbarC1,
        HotbarC2,
        HotbarC3,
        HotbarC4,
        HotbarC5,
        HotbarC6,
        HotbarC7,
        HotbarC8,
        HotbarC9,
        HotbarC0,
        // TODO - Delete these later after 8/13/21
        MoveNorthAlt,
        MoveSouthAlt,
        MoveWestAlt,
        MoveEastAlt,
        //
        DevConsole,
        //
        Reset,
        ResetToDefaults
    }
}
