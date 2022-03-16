using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public enum CommandType
    {
        Unknown = 0,
        //
        AdvColor = 1,
        ColorPattern = 2,
        //
        DisplayVariable = 4,
        SetVariable = 5,
        //
        EncyclopediaLink = 8,
        //
        SetPrintSpeed = 16,
    }
}
