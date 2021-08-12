using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs.Hotbar
{
    public abstract class HotbarAction 
    {
        public abstract bool CanUseAction();

        public abstract bool PerformAction();
    }
}
