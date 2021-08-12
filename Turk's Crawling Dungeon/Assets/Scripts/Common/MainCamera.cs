using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MainCamera : MonoBehaviour
    {
        private const int CAMERA_Z = -10;

        private CameraState state;
        private MainCursor mainCursor;

        private MainCursor MainCursor
        {
            get
            {
                if (!mainCursor)
                    mainCursor = ServiceLocator.Get<MainCursor>();
                return mainCursor;
            }
        }

        private void OnEnable()
        {
            EventManager.Listen<CursorStateChangeEvent>(this, OnCursorStateChange);
            EventManager.Listen<CursorMovedEvent>(this, OnCursorMoved);
        }

        private void OnDisable()
        {
            EventManager.StopListening<CursorStateChangeEvent>(this);
            EventManager.StopListening<CursorMovedEvent>(this);
        }

        private void OnCursorStateChange(CursorStateChangeEvent e)
        {
            CursorState cursorState = e.state;
            switch (cursorState)
            {
                case CursorState.Unlocked:
                    CameraUnlock();
                    break;
                default:
                    CameraLockToCursor();
                    break;
            }
        }
        private void CameraUnlock()
        {
            ChangeState(CameraState.Unlocked);
            transform.position = Vector3.zero.With(z: CAMERA_Z);
        }

        private void ChangeState(CameraState newState)
        {
            state = newState;
        }

        private void CameraLockToCursor()
        {
            ChangeState(CameraState.Locked);
            CenterOnCursor();
        }

        private void CenterOnCursor()
        {
            Transform mainCursorTransform = MainCursor.transform;
            transform.position = mainCursorTransform.position.With(z: CAMERA_Z);
        }

        private void OnCursorMoved(CursorMovedEvent e)
        {
            if (state != CameraState.Unlocked)
                CenterOnCursor();
        }
    }
}
