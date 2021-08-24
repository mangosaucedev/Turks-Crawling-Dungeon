using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public static class FloatingTextHandler 
    {
        public static void Draw(Vector3 position, string text) =>
            Draw(position, text, Color.yellow);

        public static void Draw(Vector3 position, string text, Color color)
        {
            GameObject gameObject = FloatingText.FromPool();
            gameObject.transform.position = position;
            FloatingText floatingText = gameObject.GetComponent<FloatingText>();
            floatingText.duration = 2f;
            floatingText.text.text = text;
            floatingText.text.color = color;
        }
    }
}
