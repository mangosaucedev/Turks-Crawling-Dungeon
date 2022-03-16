using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MainCamera : MonoBehaviour
    {
        private const int CAMERA_Z = -10;

        [SerializeField] private Camera[] cameras;
        [SerializeField] private float[] zoomSizes;

        private MainCursor mainCursor;
        private int zoomIndex;

        private MainCursor MainCursor
        {
            get
            {
                if (!mainCursor)
                    mainCursor = ServiceLocator.Get<MainCursor>();
                return mainCursor;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < zoomSizes.Length; i++)
            {
                float zoomSize = zoomSizes[i];
                if (zoomSize == Camera.main.orthographicSize)
                {
                    zoomIndex = i;
                    break;
                }
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

        public void ZoomIn()
        {
            if (zoomIndex <= 0)
                return;
            zoomIndex--;
            UpdateZoom();
        }

        private void UpdateZoom()
        {
            foreach (Camera cam in cameras)
                cam.orthographicSize = zoomSizes[zoomIndex];
        }

        public void ZoomOut()
        {
            if (zoomIndex >= zoomSizes.Length - 1)
                return;
            zoomIndex++;
            UpdateZoom();
        }
    }
}
