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
    public class FearfulPin : Talent
    {
        public override string Name => "Fearful Pin";

        public override string TalentTree => "FearfulTechniques";

        public override string IconName => "FearfulPinIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetCooldown(int level)
        {
            switch (level)
            {
                default:
                    return 18 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 18 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 17 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 16 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 15 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!obj.Parts.Has(typeof(Combat)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't pin this!", Color.red);
                return false;
            }
            return true;
        }

        protected override void OnObject()
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, target);
            bool targetHasEffects = target.Parts.TryGet(out Effects.Effects targetEffects);
            if (madeSuccessfulAttackAgainstTarget && targetHasEffects &&
                targetEffects.AddEffect(new Pinned(), TimeInfo.TIME_PER_STANDARD_TURN * GetEffectDuration(level)))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You have pinned {target.GetDisplayName()}!");
                if (target == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{target.GetDisplayName()} has pinned you!");
            }
            else if (!madeSuccessfulAttackAgainstTarget)
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You failed to pin {target.GetDisplayName()}!");
                if (target == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} failed to pin you!");
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
            $"If it hits, the enemy will be pinned to the spot for {GetEffectDuration(level)} turns.";

        private int GetEffectDuration(int level)
        {
            switch (level)
            {
                default:
                    return 4;
                case 2:
                    return 4;
                case 3:
                    return 5;
                case 4:
                    return 5;
                case 5:
                    return 6;
            }
        }

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {
                if (parent.Parts.TryGet(out Brain brain))
                    brain.Think("Decided to use fearful pin on " + e.target.GetDisplayName() + " instead.");
                target = e.target;
                ActionScheduler.EnqueueAction(parent, OnObject);
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
