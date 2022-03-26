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
    [PlayerTalent("Strangle"), Serializable]
    public class Strangle : Talent
    {
        public override string Name => "Strangle";

        public override string TalentTree => "FearfulTechniques";

        public override Sprite Icon => Assets.Get<Sprite>("StrangleIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost()
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

        public override int GetCooldown()
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

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, obj);
            bool targetFailedSavingThrow = !SavingThrows.MakeSavingThrow(parent, obj, Stat.PhysicalPower, Stat.PhysicalSave);
            bool targetHasEffects = obj.Parts.TryGet(out Effects.Effects targetEffects);
            if (madeSuccessfulAttackAgainstTarget && targetFailedSavingThrow && targetHasEffects &&
                targetEffects.AddEffect(new Strangled(), TimeInfo.TIME_PER_STANDARD_TURN * 2))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You are strangling {obj.GetDisplayName()}!");
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{obj.GetDisplayName()} is strangling you!");
            }
            else if (madeSuccessfulAttackAgainstTarget && !targetFailedSavingThrow)
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You failed to strangle {obj.GetDisplayName()}!");
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} failed to strangle you!");
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
            $"If it hits, the enemy will have to make a saving throw against your physical " +
            $"power, or be pinned to the spot and lose air for 2 turns. Consecutive attacks " +
            $"against the strangled opponent will refresh the effect for up to {GetMaxRefreshTurns()} " +
            $"turns.";

        private int GetMaxRefreshTurns()
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
                StopAllCoroutines();
                StartCoroutine(OnObjectRoutine(e.target));
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
