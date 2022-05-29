using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;
using TCD.Cinematics.Dialogues;
using TCD.Texts;
using TCD.UI.Notifications;

namespace TCD.Objects.Parts
{
    public class TutorialNpc : Part
    {
        private const int STEPS_BEFORE_MOVEMENT_2 = 1;
        private const int STATE_BEFORE_INTERACTION_TUTORIAL = 0;
        private const int STATE_COMBAT_TUTORIAL = 1;

        private int cinematicsShown;
        private bool shownMovementTutorial;
        private bool shownMovementTutorial2;
        private int stepsMoved;
        private bool shownFirstDialogueCinematic;
        private bool shownInteractionTutorial;
        private int state;

        public override string Name => "Tutorial NPC";

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<CinematicEndedEvent>(this, OnCinematicEnded);
            EventManager.Listen<PlayerMovedEvent>(this, OnPlayerMoved);
            EventManager.Listen<CinematicEventFiredEvent>(this, OnCinematicEventFired);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<CinematicEndedEvent>(this);
            EventManager.StopListening<PlayerMovedEvent>(this);
            EventManager.StopListening<CinematicEventFiredEvent>(this);
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Talk to Lulu", StartDialogue, true));
        }

        private void StartDialogue()
        {
            switch (state)
            {
                case STATE_BEFORE_INTERACTION_TUTORIAL:
                    State0Dialogue();
                    return;
                case STATE_COMBAT_TUTORIAL:
                    State1Dialogue();
                    return;
                default:
                    MessageLog.Add("Lulu grumbles something to herself.");
                    return;
            }
        }

        private void State0Dialogue()
        {
            if (DialogueHandler.GoToDialogueNode("Tutorial1"))
            {
                
            }
            else
                DialogueHandler.GoToDialogueNode("Tutorial2");
        }

        private void OnCinematicEnded(CinematicEndedEvent e)
        {
            if (!shownMovementTutorial)
            {
                shownMovementTutorial = true;
                ShowMovementTutorial();
            }
            else if (!shownInteractionTutorial && cinematicsShown == 2)
            {
                shownInteractionTutorial = true;    
                ShowInteractionTutorial();
            }

            cinematicsShown++;
        }

        private void OnPlayerMoved(PlayerMovedEvent e)
        {
            if (stepsMoved < STEPS_BEFORE_MOVEMENT_2)
                stepsMoved++;
            else if (!shownMovementTutorial2)
            {
                shownMovementTutorial2 = true;
                ShowMovementTutorial2();
            }
        }

        private void OnCinematicEventFired(CinematicEventFiredEvent e)
        {
            switch (e.e)
            {
                case "tutorialDoorOpened":
                    if (!shownFirstDialogueCinematic)
                    {
                        shownFirstDialogueCinematic = true;
                        ShowFirstDialogueCinematic();
                    }
                    break;
                case "tutorialDoorPried":
                    PoofTo(new Vector2Int(21, 11));
                    state = STATE_COMBAT_TUTORIAL;
                    break;
                default:
                    return;
            }
        }

        private void ShowMovementTutorial()
        {
            NotificationHandler.Notify(
                "Movement",
                new GameText(
                    "Use the <c=Au>Arrow Keys</> or <c=Au>Numpad Keys</> <c=R>(ensure NumLock is enabled!)</> to move.\n\n" +
                    "Alternatively, you can use <c=Au>the Mouse</> to move by left-clicking to enable " +
                    "the indicator, then left clicking again on a reachable tile that you would " +
                    "like to travel to."));
        }

        private void ShowMovementTutorial2()
        {
            NotificationHandler.Notify(
                "Movement - 2",
                new GameText(
                    "Good work! You can exit this room by leaving through the door to your north.\n\n" +
                    "<c=Au>Unlocked doors</> can be opened simply by moving into them."));
        }

        private void ShowFirstDialogueCinematic()
        { 
            ServiceLocator.Get<CinematicManager>().PlayCinematic("TutorialDialogue0A");
            ServiceLocator.Get<CinematicManager>().PlayCinematic("TutorialDialogue0B");
        }

        private void ShowInteractionTutorial()
        {
            NotificationHandler.Notify(
                "Interaction",
                new GameText(
                    "Press <c=Au>Spacebar</> and one of the <c=Au>movement keys</> to interact with an adjacent " +
                    "cell."));
        }

        private void PoofTo(Vector2Int position)
        {
            ObjectFactory.BuildFromBlueprint("TutorialPoof", Position);
            parent.cell.SetPosition(position);
            ObjectFactory.BuildFromBlueprint("TutorialPoof", Position);
        }

        private void State1Dialogue()
        {
            if (DialogueHandler.GoToDialogueNode("Tutorial1"))
            {

            }
            else
                DialogueHandler.GoToDialogueNode("Tutorial2");
        }
    }
}
