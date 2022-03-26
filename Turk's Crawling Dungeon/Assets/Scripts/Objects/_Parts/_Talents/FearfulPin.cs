using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [PlayerTalent("FearfulPin"), Serializable]
    public class FearfulPin : Talent
    {
        public override string Name => "Fearful Pin";

        public override string TalentTree => "FearfulTechniques";   

        public override Sprite Icon => Assets.Get<Sprite>("FearfulPinIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetCooldown()
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

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, obj);
            bool targetHasEffects = obj.Parts.TryGet(out Effects.Effects targetEffects);
            if (madeSuccessfulAttackAgainstTarget && targetHasEffects &&
                targetEffects.AddEffect(new Pinned(), TimeInfo.TIME_PER_STANDARD_TURN * GetEffectDuration()))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You have pinned {obj.GetDisplayName()}!");
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{obj.GetDisplayName()} has pinned you!");
            }
            else if (!madeSuccessfulAttackAgainstTarget)
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You failed to pin {obj.GetDisplayName()}!");
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} failed to pin you!");
            }
            activeCooldown += GetCooldown();
            if (parent.Parts.TryGet(out Resources resources))
                resources.ModifyResource(Resource, -GetActivationResourceCost());
            if (parent == PlayerInfo.currentPlayer)
                TimeScheduler.Tick(GetEnergyCost());
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange() => 1;

        public override string GetDescription() => $"Make an attack against an opponent. " +
            $"If it hits, the enemy will be pinned to the spot for {GetEffectDuration()} turns.";

        private int GetEffectDuration()
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
                    brain.Think("Decided to use fearul pin on " + e.target.GetDisplayName() + " instead.");
                StopAllCoroutines();
                StartCoroutine(OnObjectRoutine(e.target));
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
