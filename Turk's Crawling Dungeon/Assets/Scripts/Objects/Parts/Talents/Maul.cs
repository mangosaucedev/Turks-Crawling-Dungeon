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
    public class Maul : Talent
    {
        public override string Name => "Maul";

        public override string TalentTree => "Mangle";

        public override Sprite Icon => Assets.Get<Sprite>("MaulIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Attack;

        public override int GetActivationResourceCost()
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

        public override int GetCooldown()
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

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            bool madeSuccessfulAttackAgainstTarget = AttackHandler.AutoAttack(parent, obj);
            bool targetHasEffects = obj.parts.TryGet(out Effects.Effects targetEffects);
            bool parentHasStats = obj.parts.TryGet(out Stats parentStats);
            if (madeSuccessfulAttackAgainstTarget && AttackHandler.AutoAttack(parent, obj) && targetHasEffects &&
                parentHasStats && targetEffects.AddEffect(new Bleeding(AttackHandler.lastDamage / 2f, parentStats.RollStat(Stat.PhysicalPower)), GetBleedDuration()))
            {
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add($"You have mauled {obj.GetDisplayName()} for {AttackHandler.lastDamage} damage!");
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add($"{parent.GetDisplayName()} has mauled you for {AttackHandler.lastDamage} damage!");
            }
            activeCooldown += GetCooldown();
            if (parent.parts.TryGet(out Resources resources))
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

        public int GetBleedDuration()
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

        public override string GetDescription() => $"Make an attack against an opponent. " +
            $"If it hits, make another free attack that will cause bleed for 50% attack damage " +
            $"per turn for {((float)GetBleedDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns (totalling {50 * ((float) GetBleedDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)}% " +
            $"bleed damage).";

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (CanUseTalent() && !e.hasActed)
            {
                if (parent.parts.TryGet(out Brain brain))
                    brain.Think("Decided to maul " + e.target.GetDisplayName() + " instead.");
                StopAllCoroutines();
                StartCoroutine(OnObjectRoutine(e.target));
                e.hasActed = true;
                return false;
            }
            return base.OnAIBeforeAttack(e);
        }
    }
}
