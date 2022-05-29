using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones.Templates;

namespace TCD.Zones
{
    public abstract class TutorialTemplateMachine : ZoneTemplateMachine
    {
        private const string TEMPLATE_NAME = "Tutorial";

        protected override Vector2Int GetStartingPosition() => new Vector2Int(0, 0);

        protected override ZoneTemplate GetZoneTemplate() => Assets.Get<ZoneTemplate>(TEMPLATE_NAME);
    }
}
