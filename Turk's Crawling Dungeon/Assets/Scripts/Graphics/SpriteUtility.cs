using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Graphics
{
    public static class SpriteUtility 
    {
        public static Sprite GetSprite(string name)
        {
            if (!Assets.Exists<Sprite>(name))
            {
                DebugLogger.LogError("Could not find Sprite " + name + "! Replacing with empty sprite.");
                Texture2D texture = new Texture2D(1, 1);
                Assets.Add(name, Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero));
            }
            return Assets.Get<Sprite>(name);
        }

        public static bool IsEmpty(Sprite sprite) => (sprite.rect.width == 1 && sprite.rect.height == 1);
    }
}
