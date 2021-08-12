using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public abstract class NaturalAttack : Part
    {
        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetAttacksEvent.id)
                OnGetAttacks(e);
            return base.HandleEvent(e);
        }

        private void OnGetAttacks(LocalEvent e)
        {
            GetAttacksEvent getAttacks = (GetAttacksEvent)e;
            OnGetAttacks(getAttacks);
        }

        protected virtual void OnGetAttacks(GetAttacksEvent e)
        {

        }
    }
}
