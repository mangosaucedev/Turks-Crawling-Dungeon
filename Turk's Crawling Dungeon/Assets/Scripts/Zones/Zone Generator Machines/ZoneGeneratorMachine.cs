using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public abstract class ZoneGeneratorMachine
    {
        protected IZone Zone => CurrentZoneInfo.zone;

        protected IZoneParams ZoneParams => Zone.ZoneParams;

        protected int Width => Zone.Width;

        protected int Height => Zone.Height;

        public abstract IEnumerator Generate();
    }
}
