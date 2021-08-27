using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class Punch : JuiceAnimation
    {
        private BaseObject attacker;
        private BaseObject defender;

        public Punch(BaseObject attacker, BaseObject defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
