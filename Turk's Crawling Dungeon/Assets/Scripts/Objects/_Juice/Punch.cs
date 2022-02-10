using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class Punch : JuiceAnimation
    {
        private const float DURATION = 0.3f;

        private Vector2Int direction;
        private float duration = DURATION;
        private bool punched;

        public Punch(BaseObject obj, Vector2Int direction) : base(obj)
        {
            this.direction = direction;
        }

        public override bool IsFinished() => duration <= 0 || !obj;

        public override void Update()
        {
            base.Update();
            if (!obj)
                return;
            duration -= Time.deltaTime;
            if (duration <= DURATION / 2 && !punched)
                punched = true;
            if (!punched)
                obj.SpriteRenderer.transform.localPosition += (Vector3)(Vector2)direction * Cell.SIZE * (Time.deltaTime / DURATION);
            else
                obj.SpriteRenderer.transform.localPosition += (Vector3)(Vector2)(-direction) * Cell.SIZE * (Time.deltaTime / DURATION);           
        }

        public override void End()
        {
            base.End();
            if (!obj)
                return;
            obj.SpriteRenderer.transform.localPosition = Vector3.zero;
        }
    }
}
