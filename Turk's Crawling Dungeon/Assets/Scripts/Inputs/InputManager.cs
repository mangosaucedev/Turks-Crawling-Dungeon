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
        private static List<object> deactivatingConditions = new List<object>();

#if UNITY_EDITOR
        [SerializeField] private List<string> currentDeactivatingConditions = new List<string>();
#endif

        private MovementInterpreter movementInterpreter = new MovementInterpreter();  
        private MouseInterpreter mouseInterpreter = new MouseInterpreter();  

        public static bool IsActive => deactivatingConditions.Count == 0;

        private void Awake()
        {
            KeyEventManager.Subscribe(KeyCommand.MovePass, KeyState.PressedThisFrame, e => { Pass(); });

            KeyEventManager.Subscribe(KeyCommand.OpenInventory, KeyState.PressedThisFrame, e => { OpenView("Player Inventory View"); });
            KeyEventManager.Subscribe(KeyCommand.Cancel, KeyState.PressedThisFrame, e => { OpenView("Escape View"); });
            KeyEventManager.Subscribe(KeyCommand.OpenHelp, KeyState.PressedThisFrame, e => { OpenView("Help View"); });
            KeyEventManager.Subscribe(KeyCommand.Enter, KeyState.PressedThisFrame, e => { ConfirmAction(); });
            KeyEventManager.Subscribe(KeyCommand.Rest, KeyState.PressedThisFrame, e => { Rest(); });
            KeyEventManager.Subscribe(KeyCommand.Throw, KeyState.PressedThisFrame, e => { OpenView("Throw Object List"); });
            KeyEventManager.Subscribe(KeyCommand.ZoomIn, KeyState.PressedThisFrame, e => { ZoomIn(); });
            KeyEventManager.Subscribe(KeyCommand.ZoomOut, KeyState.PressedThisFrame, e => { ZoomOut(); });
        }

        private void Update()
        {
            UpdateActivationState();
            movementInterpreter.UpdateMovement();
            mouseInterpreter.UpdateMouse();
        }

        private void OnEnable()
        {
            EventManager.Listen<ViewOpenedEvent>(this, OnViewOpened);
            EventManager.Listen<ViewClosedEvent>(this, OnViewClosed);
        }

        private void OnDisable()
        {
            EventManager.StopListening<ViewOpenedEvent>(this);
            EventManager.StopListening<ViewClosedEvent>(this);
        }

        public static void AddDeactivatingCondition(object obj)
        {
            if (!deactivatingConditions.Contains(obj))
                deactivatingConditions.Add(obj);
            
        }

        public static void RemoveDeactivatingCondition(object obj)
        {
            if (deactivatingConditions.Contains(obj))
                deactivatingConditions.Remove(obj);
        }

        private void Pass()
        {
            if (IsActive)
                TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN);
        }

        private void UpdateActivationState()
        {
#if UNITY_EDITOR
            currentDeactivatingConditions.Clear();
#endif
            for (int i = deactivatingConditions.Count - 1; i >= 0; i--)
            {
                object obj = deactivatingConditions[i];
                if (obj == null)
                    deactivatingConditions.RemoveAt(i);
#if UNITY_EDITOR
                else
                    currentDeactivatingConditions.Add(obj.ToString());
#endif
            }
        }

        private void OnViewOpened(ViewOpenedEvent e)
        {
            if (e.view.locksInput)
                AddDeactivatingCondition(e.view);
        }

        private void OnViewClosed(ViewClosedEvent e)
        {
            if (e.view.locksInput)
                RemoveDeactivatingCondition(e.view);
        }

        private void OpenView(string viewName)
        {
            if (IsActive)
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
