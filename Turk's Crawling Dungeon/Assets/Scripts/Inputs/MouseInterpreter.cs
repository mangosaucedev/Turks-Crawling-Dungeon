using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TCD.Inputs.Actions;

namespace TCD.Inputs
{
    public class MouseInterpreter 
    {
        private PlayerActionManager PlayerActionManager =>
            ServiceLocator.Get<PlayerActionManager>();

        private MouseCursor MouseCursor =>
            ServiceLocator.Get<MouseCursor>();

        private EventSystem EventSystem => EventSystem.current;

        public void UpdateMouse()
        {
            if (Input.GetMouseButtonDown(0))
                OnLeftMouseButtonDown();
            if (Input.GetMouseButtonDown(1))
                OnRightMouseButtonDown();
        }

        private void OnLeftMouseButtonDown()
        {
            if (EventSystem.IsPointerOverGameObject())
                return;
            if (PlayerActionManager.currentAction == null)
            {
                PlayerActionManager.TryStartAction(new MoveToMouse());
            }
            else
            {
                PlayerActionManager.OnCell(MouseCursor.GetCell());
            }
        }

        private void OnRightMouseButtonDown()
        {
            if (PlayerActionManager.currentAction != null)
            {
                PlayerActionManager.CancelCurrentAction();
            }
        }
    }
}
