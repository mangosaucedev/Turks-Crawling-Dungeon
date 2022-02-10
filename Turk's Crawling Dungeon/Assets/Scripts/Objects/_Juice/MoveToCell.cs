using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class MoveToCell : JuiceAnimation
    {
        private const float DURATION = 0.3f;

        private Vector2Int direction;
        private float duration;

        public MoveToCell(BaseObject obj, Vector2Int direction) : base(obj)
        {
            this.direction = direction;
            duration = DURATION;
        }

        public override bool IsFinished() => duration <= 0 || !obj;

        public override void Start()
        {
            base.Start();
            if (!obj)
                return;
            obj.SpriteRenderer.transform.localPosition = (Vector2) (-direction * Cell.SIZE);
        }

        public override void Update()
        {
            base.Update();
            if (!obj)
                return;
            duration -= Time.deltaTime;
            obj.SpriteRenderer.transform.localPosition += (Vector3) (Vector2) direction * Cell.SIZE * (Time.deltaTime / DURATION);
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
