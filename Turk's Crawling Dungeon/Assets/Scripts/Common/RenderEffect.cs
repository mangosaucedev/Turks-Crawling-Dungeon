using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD
{
    public class RenderEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private SpriteRenderer SpriteRenderer
        {
            get
            {
                if (!spriteRenderer)
                    spriteRenderer = GetComponent<SpriteRenderer>();
                return spriteRenderer;
            }
        }
        
        public static GameObject FromPool()
        {
            return null;
        }

        public void SetSprite(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
        }
    }
}
