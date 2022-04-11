using System;
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

        public override string TalentTree => "RecklessCombat";

        public override string IconName => "TackleIcon";

        public override string Indicator => "Player To Cursor Indicator";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Object;

        public override int GetActivationResourceCost(int level)
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

        public override int GetCooldown(int level)
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

        protected override bool CanUseOnObject(BaseObject obj)
        {
            if (!parent.Parts.TryGet(out Movement movement) || !movement.CanMoveAndExitCurrentCell())
            {
                if (parent == PlayerInfo.currentPlayer)
                    FloatingTextHandler.Draw(parent.transform.position, "Can't move!", Color.red);
                return false;
            }
            return true;
        }

        protected override void OnObject()
        {
            Vector2Int startPosition = parent.cell.Position;
            Vector2Int targetPosition = target.cell.Position;
            GridRaycastResult result = GridRaycaster.Raycast(startPosition, targetPosition, new BlockedByObstaclesEvaluator());
            GridRay ray = result.ray;
            int count = ray.positions.Count;
            if (count > 2 && result.collision && result.collisionIndex - 1 <= GetRange(level))
            { 
                Vector2Int position = ray.positions[result.collisionIndex - 1];
                Movement movement = parent.Parts.Get<Movement>();
                if (movement.TryMoveToPosition(position) && (Mathf.FloorToInt(Vector2Int.Distance(Position, position)) <= 1))
                {
                    if (AttackHandler.AutoAttack(parent, target) && target.Parts.TryGet(out Effects.Effects targetEffects))
                    {
                        if (SavingThrows.MakeSavingThrow(parent, target, Stat.PhysicalPower, Stat.PhysicalSave))
                        {
                            targetEffects.AddEffect(new Prone(), TimeInfo.TIME_PER_STANDARD_TURN * 2);
                            if (parent == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You tackled {target.GetDisplayName()} to the ground!");
                            if (target == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You were tackled to the ground by {parent.GetDisplayName()}!");
                        }
                        else if (parent.Parts.TryGet(out Effects.Effects attackerEffects))
                        {
                            attackerEffects.AddEffect(new OffBalance(), TimeInfo.TIME_PER_STANDARD_TURN * 2);
                            if (parent == PlayerInfo.currentPlayer)
                                MessageLog.Add($"You failed to tackle {target.GetDisplayName()}, and are knocked off-balance!");
                            if (target == PlayerInfo.currentPlayer)
                                MessageLog.Add($"{parent.GetDisplayName()} failed to tackle you, and is knocked off-balance!");
                        }
                    }
                }
                activeCooldown = GetCooldown(level);
                if (parent.Parts.TryGet(out Resources resources))
                    resources.ModifyResource(Resource, -GetActivationResourceCost(level));
            }
            else if (count <= 2 && parent == PlayerInfo.currentPlayer)
                FloatingTextHandler.Draw(parent.transform.position, "Can't build momentum from this distance!", Color.red);
        }

        protected override bool CanUseOnCell(Cell cell) => false;

        protected override void OnCell()
        {
            
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange(int level)
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

        public override string GetDescription(int level) => $"Rush an opponent up to {GetRange(level)} cells away and try to " +
            $"tackle them to the ground. The enemy must make a saving throw against your physical power or be knocked prone " +
            $"for 2 turns. If they resist your tackle, you are thrown off-balance for 2 turns, reducing your physical save by 10.";

        protected override bool OnAIBeforeMove(AIBeforeMoveEvent e)
        {
            int distanceToTarget = Mathf.FloorToInt(Vector2Int.Distance(Position, e.targetPosition));
            if (CanUseTalent() && distanceToTarget <= GetRange(level) && distanceToTarget > 1 && !e.hasActed)
            {
                Cell cell = CurrentZoneInfo.grid[e.targetPosition];
                if (cell.Contains(out Combat combat))
                {
                    targetCell = cell;
                    ActionScheduler.EnqueueAction(parent, OnCell);
                    e.hasActed = true;
                    return false;
                }
            }
            return base.OnAIBeforeMove(e);
        }
    }
}
