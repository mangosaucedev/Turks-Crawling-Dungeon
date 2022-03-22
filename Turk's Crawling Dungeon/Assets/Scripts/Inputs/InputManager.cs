using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs.Actions;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.TimeManagement;
using TCD.UI;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.Inputs
{
    public class InputManager : MonoBehaviour
    {
        public List<IInterpreter> interpreters = new List<IInterpreter>();

        private void Awake()
        {
            interpreters.Add(new MouseInterpreter());
            interpreters.Add(new MovementInterpreter());

            KeyEventManager.Subscribe(InputGroup.None, KeyCommand.DevConsole, KeyState.PressedThisFrame, e => { OpenView("Dev Console"); });

            KeyEventManager.Subscribe(InputGroup.UI, KeyCommand.Cancel, KeyState.PressedThisFrame, e => { });

            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.MovePass, KeyState.PressedThisFrame, e => { Pass(); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.OpenInventory, KeyState.PressedThisFrame, e => { OpenView("Player Inventory View"); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.Cancel, KeyState.PressedThisFrame, e => { OpenView("Escape View"); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.OpenHelp, KeyState.PressedThisFrame, e => { OpenView("Help View"); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.Enter, KeyState.PressedThisFrame, e => { ConfirmAction(); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.Rest, KeyState.PressedThisFrame, e => { Rest(); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.Throw, KeyState.PressedThisFrame, e => { OpenView("Throw Object List"); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.ZoomIn, KeyState.PressedThisFrame, e => { ZoomIn(); });
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.ZoomOut, KeyState.PressedThisFrame, e => { ZoomOut(); });
        }

        private void Update()
        {
            foreach (IInterpreter interpreter in interpreters)
            {
                if (KeyEventManager.GetInputGroupEnabled(interpreter.InputGroup))
                    interpreter.Update();
            }
        }

        public static void SetInputGroupEnabled(InputGroup group, bool isEnabled = true) =>
            KeyEventManager.SetInputGroupEnabled(group, isEnabled);

        private void Pass() => TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN);


        private void OpenView(string viewName)
        {
            if (ViewManager.TryFind(viewName, out var view))
                ViewManager.Close(viewName);
            else
                ViewManager.Open(viewName);
        }

        private void ConfirmAction()
        {
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            if (playerActionManager.currentAction != null)
            {
                MainCursor mainCursor = ServiceLocator.Get<MainCursor>();
                Vector2Int position = mainCursor.GetGridPosition();
                if (!CurrentZoneInfo.grid.IsWithinBounds(position))
                    return;
                Cell cell = CurrentZoneInfo.grid[position];
                playerActionManager.OnCell(cell);
                playerActionManager.doNotActivateActionForFrames += 2;
            }
        }

        private void Rest()
        {
            MessageLog.Add("You begin resting.");
            PlayerLongAction longAction = new PlayerLongAction(DoRest, IsRestComplete, OnRestComplete);
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            playerActionManager.TryStartAction(longAction);
        }
        
        private bool DoRest()
        {
            TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN * 4);
            return true;
        }

        private bool IsRestComplete()
        {
            Resources resources = PlayerInfo.currentPlayer.parts.Get<Resources>();
            foreach (Resource resource in Enum.GetValues(typeof(Resource)))
            {
                float value = resources.GetResource(resource);
                float max = resources.GetMaxResource(resource);
                float percent = (float) value / max;
                float regenPoint = resources.GetBaseResourceRegenPoint(resource);
                if (percent < regenPoint)
                    return false;
            }
            return true;
        }

        private void OnRestComplete()
        {
            MessageLog.Add("You finished resting.");
        }

        private void ZoomIn()
        {
            MainCamera mainCamera = ServiceLocator.Get<MainCamera>();
            mainCamera.ZoomIn();
        }

        private void ZoomOut()
        {
            MainCamera mainCamera = ServiceLocator.Get<MainCamera>();
            mainCamera.ZoomOut();
        }
    }
}
