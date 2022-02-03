using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class MoveToCell : JuiceAnimation
    {
        private const float DURATION = 0.3f;

        private BaseObject mover;
        private Vector2Int direction;
        private float duration;

        public MoveToCell(BaseObject mover, Vector2Int direction)
        {
            this.mover = mover;
            this.direction = direction;
            duration = DURATION;
        }

        public override bool IsFinished() => duration <= 0 || !mover;

        public override void Start()
        {
            base.Start();
            mover.SpriteRenderer.transform.localPosition = (Vector2) (-direction * Cell.SIZE);
        }

        public override void Update()
        {
            base.Update();
            if (!mover)
                return;
            duration -= Time.deltaTime;
            mover.SpriteRenderer.transform.localPosition += (Vector3) (Vector2) direction * Cell.SIZE * (Time.deltaTime / DURATION);
        }

        public override void End()
        {
            base.End();
            if (!mover)
                return;
            mover.SpriteRenderer.transform.localPosition = Vector3.zero;
        }
    }
}
