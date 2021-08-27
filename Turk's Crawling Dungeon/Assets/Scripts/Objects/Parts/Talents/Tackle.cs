using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Objects.Parts.Effects;
using TCD.Texts;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class Tackle : Talent
    {
        public override string Name => "Tackle";

        public override Sprite Icon => Assets.Get<Sprite>("TackleIcon");

        public override string Indicator => "Player To Cursor Indicator";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Object;

        public override int GetActivationResourceCost()
        {
            switch (level)
            {
                default:
                    return 12;
                case 2:
                    return 13;
                case 3:
                    return 14;
                case 4:
                    return 15;
                case 5:
                    return 16;
            }
        }

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 12 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2: 
                    return 11 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 10 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 9 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 8 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            if (!parent.parts.TryGet(out Movement movement))
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't move!", Color.red);
                yield break;
            }
            Vector2Int startPosition = parent.cell.Position;
            Vector2Int targetPosition = obj.cell.Position;
            GridRaycastResult result = GridRaycaster.Raycast(startPosition, targetPosition, new BlockedByObstaclesEvaluator());
            GridRay ray = result.ray;
            int count = ray.positions.Count;
            if (count > 2 && result.collision && result.collisionIndex - 1 <= GetRange())
            { 
                Vector2Int position = ray.positions[result.collisionIndex - 1];
                if (movement.TryMoveToPosition(position))
                {
                    if (AttackHandler.AutoAttack(parent, obj) && obj.parts.TryGet(out Effects.Effects targetEffects))
                    {
                        if (SavingThrows.MakeSavingThrow(parent, obj, Stat.PhysicalPower, Stat.PhysicalSave))
                        {
                            targetEffects.AddEffect(new Prone(), TimeInfo.TIME_PER_STANDARD_TURN * 2);
                            if (parent == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You tackled {obj.display.GetDisplayName()} to the ground!");
                            if (obj == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You were tackled to the ground by {parent.display.GetDisplayName()}!");
                        }
                        else if (parent.parts.TryGet(out Effects.Effects attackerEffects))
                        {
                            attackerEffects.AddEffect(new OffBalance(), TimeInfo.TIME_PER_STANDARD_TURN * 2);
                            if (parent == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You failed to tackle {obj.display.GetDisplayName()}, and are knocked off-balance!");
                            if (obj == PlayerInfo.currentPlayer)
                                MessageLog.Add($"{parent.display.GetDisplayName()} failed to tackle you, and is knocked off-balance!");
                        }
                    }
                }
                activeCooldown += GetCooldown();
                if (parent.parts.TryGet(out Resources resources))
                    resources.ModifyResource(Resource, -GetActivationResourceCost());
                if (parent == PlayerInfo.currentPlayer)
                    TimeScheduler.Tick(GetEnergyCost());
            }
            else if (count <= 2 && parent == PlayerInfo.currentPlayer)
                FloatingTextHandler.Draw(parent.transform.position, "Can't build momentum from this distance!", Color.red);
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange()
        {
            switch (level)
            {
                default:
                    return 4;
                case 2:
                    return 5;
                case 3:
                    return 6;
                case 4:
                    return 7;
                case 5:
                    return 8;
            }
        }

        public override string GetDescription() => $"Rush an opponent up to {GetRange()} cells away and try to " +
            $"tackle them to the ground. The enemy must make a saving throw against your physical power or be knocked prone " +
            $"for 2 turns. If they resist your tackle, you are thrown off-balance for 2 turns, reducing your physical save by 10.";

        protected override bool OnAIBeforeMove(AIBeforeMoveEvent e)
        {
            int distanceToTarget = Mathf.FloorToInt(Vector2Int.Distance(Position, e.targetPosition));
            if (CanUseTalent() && distanceToTarget <= GetRange() && distanceToTarget > 1 && !e.hasActed)
            {
                Cell cell = CurrentZoneInfo.grid[e.targetPosition];
                if (cell.Contains(out Combat combat))
                {
                    StopAllCoroutines();
                    StartCoroutine(OnObjectRoutine(combat.parent));
                    e.hasActed = true;
                    return false;
                }
            }
            return base.OnAIBeforeMove(e);
        }
    }
}
