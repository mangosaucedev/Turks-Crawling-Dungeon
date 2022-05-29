using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class TutorialCrowbarSpawner : Part
    {
        public override string Name => "Tutorial Crowbar Spawner";

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<CinematicEventFiredEvent>(this, OnCinematicEventFired);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<CinematicEventFiredEvent>(this);
        }

        private void OnCinematicEventFired(CinematicEventFiredEvent e)
        {
            if (e.e == "tutorialSpawnCrowbar")
                SpawnCrowbar();
        }

        private void SpawnCrowbar()
        {
            ObjectFactory.BuildFromBlueprint("TutorialCrowbar", Position);
            GameText text = new GameText(
                "Press <c=Au>Spacebar</> and the corresponding <c=Au>movement key</> to interact with the crowbar. " +
                "Then select the option to <c=Au>get</> or <c=Au>equip</> the crowbar before proceeding.");
            ActionScheduler.EnqueueAction(PlayerInfo.currentPlayer, () => { MessageLog.Add(text);});
            parent.Destroy();
        }
    }
}
