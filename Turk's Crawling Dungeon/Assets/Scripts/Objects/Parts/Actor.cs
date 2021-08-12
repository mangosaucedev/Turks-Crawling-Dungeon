using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    public class Actor : Part
    {
        public int energy;

        public override string Name => "Actor";

        protected override void OnEnable()
        {
            base.OnEnable();
            TimeScheduler.AddActor(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();   
            TimeScheduler.RemoveActor(this);
        }

        public void Act(int turnEnergy)
        {
            ActorTakeTurnEvent e = LocalEvent.Get<ActorTakeTurnEvent>();
            energy += turnEnergy;
            e.energy = energy;
            FireEvent(parent, e);
        }
    }
}
