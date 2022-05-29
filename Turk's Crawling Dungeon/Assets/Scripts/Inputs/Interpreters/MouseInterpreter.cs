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
            if (Input.mouseScrollDelta.y > 0)
                ZoomIn();
            if (Input.mouseScrollDelta.y < 0)
                ZoomOut();
        }

        private void ZoomIn() => ServiceLocator.Get<MainCamera>().ZoomIn();

        private void ZoomOut() => ServiceLocator.Get<MainCamera>().ZoomOut();

        private void OnLeftMouseButtonDown()
        {
            if (EventSystem.IsPointerOverGameObject() || !KeyEventManager.GetInputGroupEnabled(InputGroup.Gameplay))
                return;
            if (PlayerActionManager.currentAction == null)
                PlayerActionManager.TryStartAction(new MoveToMouse());
            PlayerActionManager.OnCell(MouseCursor.GetCell());
        }

        private void OnRightMouseButtonDown()
        {
            if (PlayerActionManager.currentAction != null)
                PlayerActionManager.CancelCurrentAction();
        }
    }
}
