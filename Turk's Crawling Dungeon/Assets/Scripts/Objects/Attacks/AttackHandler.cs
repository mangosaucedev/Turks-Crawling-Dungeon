using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects.Attacks
{
    public static class AttackHandler
    {
        private static BaseObject currentAttacker;
        private static BaseObject currentDefender;
        private static Attack currentAttack;

        public static bool AutoAttack(BaseObject attacker, BaseObject defender)
        {
            currentAttacker = attacker;
            currentDefender = defender;
            if (!TryGetAttack(currentDefender, out currentAttack) || !CanAttackTarget())
                return false;
            return AttackTarget();
        }

        public static bool TryGetAttack(BaseObject target, out Attack attack)
        {
            GetAttacksEvent e = LocalEvent.Get<GetAttacksEvent>();
            e.obj = currentAttacker;
            e.target = target;
            currentAttacker.HandleEvent(e);
            attack = e.GetAttack();
            if (attack == null)
                return false;
            if (currentAttacker.parts.TryGet(out Stats stats))
                attack.hitAccuracy = stats.RollStat(Stat.HitAccuracy);
            return true;
        }

        private static bool CanAttackTarget()
        {
            BeforeAttackEvent e = LocalEvent.Get<BeforeAttackEvent>();
            e.obj = currentAttacker;
            e.target = currentDefender;
            e.attack = currentAttack;
            if (!currentDefender.HandleEvent(e) || !currentAttacker.HandleEvent(e))
            {
                if (currentAttacker == PlayerInfo.currentPlayer)
                    MessageLog.Add($"Your attack against {currentDefender.display.GetDisplayName()} {e.Result}!");
                if (currentDefender == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentAttacker.display.GetDisplayName()}'s attack against you {e.Result}!");
                return false;
            }
            return true;
        }

        public static bool AttackTarget(BaseObject attacker, BaseObject defender, Attack attack)
        {
            currentAttacker = attacker;
            currentDefender = defender;
            currentAttack = attack;
            if (!CanAttackTarget())
                return false;
            return AttackTarget();
        }

        private static bool AttackTarget()
        {
            float damage = Random.Range(currentAttack.minDamage, currentAttack.maxDamage);
            if (currentAttack.attackType == AttackType.Physical)
                damage = DamageHandler.AddPhysicalPowerToDamage(damage, currentAttacker);
            else if (currentAttack.attackType == AttackType.Mental)
                damage = DamageHandler.AddMentalPowerToDamage(damage, currentAttacker);
            damage = DamageHandler.PerformReductionsOnDamage(damage, currentAttack.damageType, currentDefender);

            if (damage <= 0)
            {
                if (currentAttacker == PlayerInfo.currentPlayer)
                    MessageLog.Add($"Your attack against {currentDefender.display.GetDisplayName()} failed to penetrate!");
                if (currentDefender == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentAttacker.display.GetDisplayName()}'s attack against you failed to penetrate!");
                return false;
            }

            if (!currentDefender.parts.TryGet(out Hitpoints defenderHitpoints))
            {
                if (currentAttacker == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentDefender.display.GetDisplayName()} cannot be attacked.");
                return false;
            }

            string message = $"{currentAttacker.display.GetDisplayName()} {currentAttack.verbPastTense} " +
                $"{currentDefender.display.GetDisplayName()} for {damage.RoundToDecimal(1)} " +
                $"{currentAttack.damageType.name.ToLower()} damage!";
            MessageLog.Add(message);
            defenderHitpoints.ModifyHp(-damage);
            AttackedEvent e = LocalEvent.Get<AttackedEvent>();
            e.obj = currentDefender;
            e.attacker = currentAttacker;
            e.attack = currentAttack;
            e.damage = damage;
            currentDefender.HandleEvent(e);
            return true;
        }
    }
}
