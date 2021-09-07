using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.Objects.Parts;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.Objects.Attacks
{
    public static class AttackHandler
    {
        public static float lastDamage;

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
                    MessageLog.Add($"Your attack against {currentDefender.GetDisplayName()} {e.Result}!");
                if (currentDefender == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentAttacker.GetDisplayName()}'s attack against you {e.Result}!");
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
            lastDamage = Random.Range(currentAttack.minDamage, currentAttack.maxDamage);
            if (currentAttack.attackType == AttackType.Physical)
                lastDamage = DamageHandler.AddPhysicalPowerToDamage(lastDamage, currentAttacker);
            else if (currentAttack.attackType == AttackType.Mental)
                lastDamage = DamageHandler.AddMentalPowerToDamage(lastDamage, currentAttacker);
            lastDamage = DamageHandler.PerformReductionsOnDamage(lastDamage, currentAttack.damageType, currentDefender);

            if (lastDamage <= 0)
            {
                if (currentAttacker == PlayerInfo.currentPlayer)
                    MessageLog.Add($"Your attack against {currentDefender.GetDisplayName()} failed to penetrate!");
                if (currentDefender == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentAttacker.GetDisplayName()}'s attack against you failed to penetrate!");
                return false;
            }

            if (!currentDefender.parts.TryGet(out Resources defenderResources))
            {
                if (currentAttacker == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{currentDefender.GetDisplayName()} cannot be attacked.");
                return false;
            }

            string message = $"{currentAttacker.GetDisplayName()} {currentAttack.verbPastTense} " +
                $"{currentDefender.GetDisplayName()} for {lastDamage.RoundToDecimal(1)} " +
                $"{currentAttack.damageType.name.ToLower()} damage!";
            MessageLog.Add(message);
            AttackEvent attackEvent = LocalEvent.Get<AttackEvent>();
            attackEvent.obj = currentAttacker;
            attackEvent.defender = currentDefender;
            attackEvent.attack = currentAttack;
            attackEvent.damage = lastDamage;
            currentAttacker.HandleEvent(attackEvent);
            AttackedEvent attackedEvent = LocalEvent.Get<AttackedEvent>();
            attackedEvent.obj = currentDefender;
            attackedEvent.attacker = currentAttacker;
            attackedEvent.attack = currentAttack;
            attackedEvent.damage = lastDamage;
            currentDefender.HandleEvent(attackedEvent);
            if (currentDefender.parts.TryGet(out Render render))
                render.RenderEffect("Standard Render Effect", Choose.Random("EnemyHitGraphic", "EnemyHitGraphic1", "EnemyHitGraphic2"), 0.2f);
            if (currentDefender == PlayerInfo.currentPlayer)
                FloatingTextHandler.DrawFlying(currentDefender.transform.position, $"{lastDamage.RoundToDecimal(1)}", Color.red);
            else
                FloatingTextHandler.DrawFlying(currentDefender.transform.position, $"{lastDamage.RoundToDecimal(1)}", Color.green);
            defenderResources.ModifyResource(Resource.Hitpoints, -lastDamage);
            return true;
        }
    }
}
