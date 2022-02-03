using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class Punch : JuiceAnimation
    {
        private const float DURATION = 0.3f;

        private BaseObject attacker;
        private Vector2Int direction;
        private float duration = DURATION;
        private bool punched;

        public Punch(BaseObject attacker, Vector2Int direction)
        {
            this.attacker = attacker;
            this.direction = direction;
        }

        public override bool IsFinished() => duration <= 0 || !attacker;

        public override void Update()
        {
            base.Update();
            if (!attacker)
                return;
            duration -= Time.deltaTime;
            if (duration <= DURATION / 2 && !punched)
                punched = true;
            if (!punched)
                attacker.SpriteRenderer.transform.localPosition += (Vector3)(Vector2)direction * Cell.SIZE * (Time.deltaTime / DURATION);
            else
                attacker.SpriteRenderer.transform.localPosition += (Vector3)(Vector2)(-direction) * Cell.SIZE * (Time.deltaTime / DURATION);           
        }

        public override void End()
        {
            base.End();
            if (!attacker)
                return;
            attacker.SpriteRenderer.transform.localPosition = Vector3.zero;
        }
    }
}
