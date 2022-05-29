using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCD.UI.Tooltips
{
    public class Tooltip : MonoBehaviour
    {
        private const int CHARACTER_WRAP_LIMIT = 32;
        
        public Text header;
        public TextMeshProUGUI body;
        
        [SerializeField] private LayoutElement layoutElement;

        private RectTransform RectTransform => (RectTransform) transform;

        private void Start()
        {
            if (string.IsNullOrEmpty(header.text))
                header.gameObject.SetActive(false);
            int headerLength = header.text.Length;
            int bodyLength = body.text.Length;
            bool layoutElementEnabled = headerLength > CHARACTER_WRAP_LIMIT || bodyLength > CHARACTER_WRAP_LIMIT;
            layoutElement.enabled = layoutElementEnabled;
            PositionOnMouse();
        }

        private void PositionOnMouse()
        {
            Vector2 position = Input.mousePosition;
            int pivotX = Mathf.RoundToInt(position.x / Screen.width);
            int pivotY = Mathf.RoundToInt(position.y / Screen.height);
            RectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = Camera.main.ScreenToWorldPoint(position).With(z:0);
        }
    }
}
