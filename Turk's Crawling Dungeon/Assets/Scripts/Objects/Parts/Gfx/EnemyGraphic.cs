using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class EnemyGraphic : Graphic
    {
        public override string Name => "Enemy Graphic";

        protected override Sprite Sprite => Assets.Get<Sprite>("EnemyGraphic");

        protected override void Start()
        {
            base.Start();
            graphicRenderer.drawMode = SpriteDrawMode.Sliced;
            Vector2 spriteSize = parent.SpriteRenderer.sprite.rect.size;
            spriteSize /= parent.SpriteRenderer.sprite.pixelsPerUnit;
            float xSize = Mathf.Max(spriteSize.x, 2);
            float ySize = Mathf.Max(spriteSize.y, 2);
            graphicRenderer.size = new Vector2(xSize, ySize);
        }
    }
}
