using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class AIQuery
    {
        public Brain brain;
        public AIFinder finder;
        public AIDistance distance;
        public AIAttack attack;

        public AIQuery(Brain brain)
        {
            this.brain = brain;
            finder = new AIFinder(this.brain);
            distance = new AIDistance(this.brain);
            attack = new AIAttack(this.brain);
        }
    }
}