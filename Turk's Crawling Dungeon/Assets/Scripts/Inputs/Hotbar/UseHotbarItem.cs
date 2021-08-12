using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Inputs.Hotbar
{
    public class UseHotbarItem : HotbarAction
    {
        public BaseObject item;

        public UseHotbarItem(BaseObject item)
        {
            this.item = item;
        }

        public override bool CanUseAction()
        {
            return true;
        }

        public override bool PerformAction()
        {
            return true;
        }
    }
}
