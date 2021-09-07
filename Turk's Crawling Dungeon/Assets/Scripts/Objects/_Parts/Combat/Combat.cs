using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Texts;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Combat : Part
    {
        public BaseObject lastDamagedBy;
        public string lastDamagedByDisplayName;
        public Attack lastAttackRecieved;
        public float lastAttackDamageRecieved;
        
        [SerializeField] private Attack lastAttemptedAttack;
        
        public override string Name => "Combat";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetAttacksEvent.id)
                OnGetAttacks(e);
            if (e.Id == AttackedEvent.id)
                OnAttacked(e);
            if (e.Id == DestroyObjectEvent.id)
                OnDestroyObject();
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
            e.AddAttack(defaultAttack, 0.001f);
        }

        private void OnAttacked(LocalEvent e)
        {
            AttackedEvent attackedEvent = (AttackedEvent)e;
            OnAttacked(attackedEvent);
        }

        private void OnAttacked(AttackedEvent e)
        {
            lastAttackRecieved = e.attack;
            lastAttackDamageRecieved = e.damage;
            lastDamagedBy = e.attacker;
            lastDamagedByDisplayName = lastDamagedBy.GetDisplayName();
        }

        private void OnDestroyObject()
        {
            if (lastDamagedBy == PlayerInfo.currentPlayer && parent != PlayerInfo.currentPlayer)
            {
                Color killedMessageColor = new Color(1, 0.2f, 0.78f);
                if (lastAttackDamageRecieved > 0)
                    FloatingTextHandler.DrawFlying(parent.transform.position, $"Killed! ({lastAttackDamageRecieved.RoundToDecimal(1)})", killedMessageColor);
                else
                    FloatingTextHandler.DrawFlying(parent.transform.position, $"Killed!", killedMessageColor);
            }
        }

        public int GetAttackCost(BaseObject target)
        {
            if (parent.parts.TryGet(out Stats stats))
                return stats.GetStatLevel(Stat.AttackCost);
            return TimeInfo.TIME_PER_STANDARD_TURN;
        }
    }
}
