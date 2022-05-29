using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;

namespace TCD.Objects.Parts
{
    public class TutorialDoor1 : Part
    {
        public override string Name => "Tutorial Door 1";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            if (PlayerHasCrowbar())
                e.interactions.Add(new Interaction("Pry open", PryOpen));
        }

        private bool PlayerHasCrowbar()
        {
            foreach (BaseObject item in PlayerInfo.currentPlayer.Parts.Get<Inventory>().items)
            {
                if (item.name == "TutorialCrowbar")
                    return true;
            }
            return false;
        }

        private void PryOpen()
        {
            ObjectFactory.BuildFromBlueprint("TutorialDoor", Position);
            parent.Destroy();
            ServiceLocator.Get<CinematicManager>().FireEvent("tutorialDoorPried");
        }
    }
}
