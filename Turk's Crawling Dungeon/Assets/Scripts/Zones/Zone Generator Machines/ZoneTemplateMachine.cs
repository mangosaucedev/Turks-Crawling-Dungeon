using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Templates;

namespace TCD.Zones
{
    public abstract class ZoneTemplateMachine : ZoneGeneratorMachine
    {
        protected abstract ZoneTemplate GetZoneTemplate();

        protected abstract Vector2Int GetStartingPosition();

        protected Vector2Int LocalToGlobal(Vector2Int local) => 
            GetStartingPosition() + local;

        protected Vector2Int GlobalToLocal(Vector2Int global) =>
            global - GetStartingPosition();
    }
}
