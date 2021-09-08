using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class Punch : JuiceAnimation
    {
        private BaseObject attacker;
        private Vector2Int direction;
        private float duration = 0.3f;

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
            attacker.transform.Translate((Vector2) direction * 0.5f * Time.deltaTime, Space.World);
        }
    }
}
