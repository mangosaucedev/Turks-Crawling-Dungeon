using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs.Actions;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.TimeManagement;
using TCD.UI;

namespace TCD.Inputs
{
    public class InputManager : MonoBehaviour
    {
        private static List<object> deactivatingConditions = new List<object>();

#if UNITY_EDITOR
        [SerializeField] private List<string> currentDeactivatingConditions = new List<string>();
#endif

        private MovementInterpreter movementInterpreter = new MovementInterpreter();  

        public static bool IsActive => deactivatingConditions.Count == 0;

        private void Awake()
        {
            KeyEventManager.Subscribe(KeyCommand.MovePass, KeyState.PressedThisFrame, e => { Pass(); });

            KeyEventManager.Subscribe(KeyCommand.OpenInventory, KeyState.PressedThisFrame, e => { OpenView("Player Inventory View"); });
            KeyEventManager.Subscribe(KeyCommand.Cancel, KeyState.PressedThisFrame, e => { OpenView("Escape View"); });
            KeyEventManager.Subscribe(KeyCommand.OpenHelp, KeyState.PressedThisFrame, e => { OpenView("Help View"); });
        }

        private void Update()
        {
            UpdateActivationState();
            movementInterpreter.UpdateMovement();
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
    }
}
