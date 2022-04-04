using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD
{
    [RequireComponent(typeof(Image))]
    public class ConstrainedImage : MonoBehaviour
    {
        [SerializeField] private RectTransform parent;
        [SerializeField] private Image image;

        private void Start()
        {
            if (!image.sprite)
                return;
            Rect rect = parent.rect;
            Rect imageRect = image.sprite.rect;
            float scale = Mathf.Min(rect.width / imageRect.width, rect.height / imageRect.height);
            float width = imageRect.width * scale;
            float height = imageRect.height * scale;
            image.rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}
