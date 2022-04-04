using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TCD.Inputs.Actions;

namespace TCD.Inputs
{
    public class MouseInterpreter : IInterpreter
    {
        private PlayerActionManager PlayerActionManager =>
            ServiceLocator.Get<PlayerActionManager>();

        private MouseCursor MouseCursor =>
            ServiceLocator.Get<MouseCursor>();

        public InputGroup InputGroup => InputGroup.Gameplay;

        private EventSystem EventSystem => EventSystem.current;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnLeftMouseButtonDown();
            if (Input.GetMouseButtonDown(1))
                OnRightMouseButtonDown();
        }

        private void OnLeftMouseButtonDown()
        {
            if (EventSystem.IsPointerOverGameObject() || !KeyEventManager.GetInputGroupEnabled(InputGroup.Gameplay)) 
                return;
            if (PlayerActionManager.currentAction == null)
                PlayerActionManager.TryStartAction(new MoveToMouse());
            else
                PlayerActionManager.OnCell(MouseCursor.GetCell());
        }

        private void OnRightMouseButtonDown()
        {
            if (PlayerActionManager.currentAction != null)
                PlayerActionManager.CancelCurrentAction();
        }
    }
}
