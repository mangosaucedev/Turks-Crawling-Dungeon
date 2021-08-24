using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MainCamera : MonoBehaviour
    {
        private const int CAMERA_Z = -10;

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
            EventManager.Listen<CursorMovedEvent>(this, OnCursorMoved);
        }

        private void OnDisable()
        {
            EventManager.StopListening<CursorMovedEvent>(this);
        }


        private void CenterOnCursor()
        {
            Transform mainCursorTransform = MainCursor.transform;
            transform.position = mainCursorTransform.position.With(z: CAMERA_Z);
        }

        private void OnCursorMoved(CursorMovedEvent e)
        {
            CenterOnCursor();
        }
    }
}
