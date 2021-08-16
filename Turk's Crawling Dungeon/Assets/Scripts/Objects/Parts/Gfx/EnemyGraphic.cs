using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
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
            graphicRenderer.size = spriteSize;
        }
    }
}
