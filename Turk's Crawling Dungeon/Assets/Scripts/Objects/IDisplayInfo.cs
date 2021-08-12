using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public interface IDisplayInfo
    { 
        string GetDisplayName();

        string GetDisplayNamePlural();

        string GetDescription();

        string GetDescriptionShort();
    }
}
