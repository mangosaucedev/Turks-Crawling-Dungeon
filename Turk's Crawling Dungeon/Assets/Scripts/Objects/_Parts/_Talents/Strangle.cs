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
    public class Strangle : Talent
    {
        public override string Name => "Strangle";

        public override string TalentTree => "FearfulTechniques";

        public override string IconName => "StrangleIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost(int level)
        {
            switch (level)
            {
                default:
                    return 16;
                case 2:
                    return 16;
                case 3:
                    return 16;
                case 4:
                    return 16;
                case 5:
                    return 16;
            }
        }

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 28 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 26 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 24 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 22 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 20 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!obj.Parts.Has(typeof(Combat)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't strangle this!", Color.red);
                return false;
            }
            return true;
        }

        protected override void OnObject()
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, target);
            bool targetFailedSavingThrow = !SavingThrows.MakeSavingThrow(parent, target, Stat.PhysicalPower, Stat.PhysicalSave);
            bool targetHasEffects = target.Parts.TryGet(out Effects.Effects targetEffects);
            if (madeSuccessfulAttackAgainstTarget && targetFailedSavingThrow && targetHasEffects &&
                targetEffects.AddEffect(new Strangled(), TimeInfo.TIME_PER_STANDARD_TURN * 2))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You are strangling {target.GetDisplayName()}!");
                if (target == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{target.GetDisplayName()} is strangling you!");
            }
            else if (madeSuccessfulAttackAgainstTarget && !targetFailedSavingThrow)
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You failed to strangle {target.GetDisplayName()}!");
                if (target == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} failed to strangle you!");
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
        
        public override string GetDescription(int level) => $"Make an attack against an opponent. " +
            $"If it hits, the enemy will have to make a saving throw against your physical " +
            $"power, or be pinned to the spot and lose air for 2 turns. Consecutive attacks " +
            $"against the strangled opponent will refresh the effect for up to {GetMaxRefreshTurns(level)} " +
            $"turns.";

        private int GetMaxRefreshTurns(int level)
        {
            switch (level)
            {
                default:
                    return 3;
                case 2:
                    return 3;
                case 3:
                    return 4;
                case 4:
                    return 4;
                case 5:
                    return 5;
            }
        }

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {
                if (parent.Parts.TryGet(out Brain brain))
                    brain.Think("Decided to strangle " + e.target.GetDisplayName() + " instead.");
                target = e.target;
                ActionScheduler.EnqueueAction(parent, OnObject);
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
