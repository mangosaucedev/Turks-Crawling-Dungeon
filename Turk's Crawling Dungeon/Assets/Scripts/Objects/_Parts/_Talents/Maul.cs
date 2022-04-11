using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.Texts;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Maul : Talent
    {
        public override string Name => "Maul";

        public override string TalentTree => "Mangle";

        public override string IconName => "MaulIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
        {
            switch (level)
            {
                default:
                    return 24;
                case 2:
                    return 26;
                case 3:
                    return 28;
                case 4:
                    return 30;
                case 5:
                    return 32;
            }
        }

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 22 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 20 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 19 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 17 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 16 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!obj.Parts.Has(typeof(Combat)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't maul this!", Color.red);
                return false;
            }
            return true;
        }

        protected override void OnObject()
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, target);
            bool targetHasEffects = target.Parts.TryGet(out Effects.Effects targetEffects);
            bool parentHasStats = target.Parts.TryGet(out Stats parentStats);

            if (madeSuccessfulAttackAgainstTarget && AttackHandler.AutoAttack(parent, target) && targetHasEffects &&
                parentHasStats && targetEffects.AddEffect(new Bleeding(AttackHandler.lastDamage / 2f, parentStats.RollStat(Stat.PhysicalPower)), GetBleedDuration(level)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You have mauled {target.GetDisplayName()} for {AttackHandler.lastDamage} damage!");
                if (target == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} has mauled you for {AttackHandler.lastDamage} damage!");
            }

            activeCooldown += GetCooldown(level);

            if (parent.Parts.TryGet(out Resources resources))
                resources.ModifyResource(Resource, -GetActivationResourceCost(level));
        }

        protected override bool CanUseOnCell(Cell cell) => false;

        protected override void OnCell()
        {

        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level) => 1;

        public int GetBleedDuration(int level)
        {
            switch (level)
            {
                default:
                    return 2 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 3 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 4 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription(int level) => $"Make an attack against an opponent. " +
            $"If it hits, make another free attack that will cause bleed for 50% attack damage " +
            $"per turn for {((float)GetBleedDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns (totalling {50 * ((float) GetBleedDuration(level) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)}% " +
            $"bleed damage).";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {
                if (parent.Parts.TryGet(out Brain brain))
                    brain.Think("Decided to maul " + e.target.GetDisplayName() + " instead.");
                target = e.target;
                ActionScheduler.EnqueueAction(parent, OnObject);
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
