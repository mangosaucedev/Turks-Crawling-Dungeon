using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;

namespace TCD.Objects.Parts
{
    public class Injuries : Part
    {
        public readonly List<IInjury> injuries = new List<IInjury>();

        public override string Name => "Injuries";

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            for (int i = injuries.Count - 1; i >= 0; i--)
            {
                IInjury injury = injuries[i];
                if (injury.PassTime(e.timeElapsed))
                {
                    injury.OnHealed();
                    injuries.RemoveAt(i);
                }
            }
        }

        public void AddInjury(IInjury injury)
        {
            injury.Injuries = this;
            injury.OnAcquire();
            injuries.Add(injury);

            if (parent == PlayerInfo.currentPlayer)
            {
                FloatingTextHandler.Draw(parent.transform.position, "Injured!", Color.magenta);
                MessageLog.Add("You've been seriously injured!");
            }
        }
    }
}
