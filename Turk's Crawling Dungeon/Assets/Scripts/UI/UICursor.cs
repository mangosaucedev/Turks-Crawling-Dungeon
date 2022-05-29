using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TCD.UI
{
    public class UICursor : MonoBehaviour
    {
        public Vector2 position;

        private void LateUpdate()
        {
            GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
            if (selectedGameObject)
                position = selectedGameObject.transform.position;
        }
    }
}
