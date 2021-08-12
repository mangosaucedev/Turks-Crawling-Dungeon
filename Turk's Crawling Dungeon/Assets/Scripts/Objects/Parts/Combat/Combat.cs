using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class Combat : Part
    {
        public BaseObject lastDamagedBy;
        public string lastDamagedByDisplayName;
        
        private Attack lastAttemptedAttack;

        public override string Name => "Combat";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetAttacksEvent.id)
                OnGetAttacks(e);
            if (e.Id == AttackedEvent.id)
                OnAttacked(e);
            return base.HandleEvent(e);
        }

        private void OnGetAttacks(LocalEvent e)
        {
            GetAttacksEvent getAttacks = (GetAttacksEvent)e;
            OnGetAttacks(getAttacks);
        }

        private void OnGetAttacks(GetAttacksEvent e)
        {
            Attack defaultAttack = AttackFactory.BuildFromBlueprint("WeakShove");
            defaultAttack.weapon = parent;
            e.AddAttack(defaultAttack, 0.1f);
        }

        private void OnAttacked(LocalEvent e)
        {
            AttackedEvent attackedEvent = (AttackedEvent)e;
            OnAttacked(attackedEvent);
        }

        private void OnAttacked(AttackedEvent e)
        {
            lastDamagedBy = e.attacker;
            lastDamagedByDisplayName = lastDamagedBy.display.GetDisplayName();
        }
    }
}
