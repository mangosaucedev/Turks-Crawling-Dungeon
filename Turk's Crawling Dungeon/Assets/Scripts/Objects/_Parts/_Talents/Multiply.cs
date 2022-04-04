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
    [PlayerTalent("Multiply"), Serializable]
    public class Multiply : Talent
    {
        public bool isClone;
        public List<BaseObject> clones = new List<BaseObject>();

        public override string Name => "Multiply";

        public override string TalentTree => "StrangeTechniques";

        public override string IconName => "MultiplyIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.Cell;

        public override int GetCooldown()
        {
            switch (level)
            {
                default:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            TrimClonesList();
            if (clones.Count >= GetMaxClones())
                yield break;
            if (!cell.Contains(out Obstacle obstacle) || !obstacle.IsImpassable)
            {
                BaseObject clone = ObjectFactory.BuildFromBlueprint(parent.name, cell.Position);
                clones.Add(clone);
                if (clone.Parts.TryGet(out Multiply multiply))
                    multiply.isClone = true;
                if (parent == PlayerInfo.currentPlayer)
                    MessageLog.Add("You multiplied!");
                activeCooldown += GetCooldown();
            }
        }

        private void TrimClonesList()
        {
            for (int i = clones.Count - 1; i >= 0; i--)
            {
                if (clones[i] == null)
                    clones.RemoveAt(i);
            }
        }

        public override int GetEnergyCost() => TimeInfo.TIME_PER_STANDARD_TURN;

        public override int GetRange() => 1;

        public override string GetDescription() => $"Clone yourself into an adjacent cell " +
            $"(up to {GetMaxClones()} times!)";

        private int GetMaxClones()
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

        protected override bool OnAIBeforeMove(AIBeforeMoveEvent e)
        {
            if (TryUseAbility(e))
                return false;
            return base.OnAIBeforeMove(e);
        }

        private bool TryUseAbility(AICommandEvent e)
        {
            TrimClonesList();
            if (clones.Count >= GetMaxClones())
                return false;
            if (CanUseTalent() && !e.hasActed && !isClone && TryGetEmptyAdjacentPosition(out Vector2Int position))
            {
                Cell cell = CurrentZoneInfo.grid[position];
                StopAllCoroutines();
                StartCoroutine(OnCellRoutine(cell));
                e.hasActed = true;
                return true;
            }
            return false;
        }

        private bool TryGetEmptyAdjacentPosition(out Vector2Int position)
        {
            position = Vector2Int.zero;
            List<Vector2Int> emptyPositions = new List<Vector2Int>();
            int xMin = Position.x - 1;
            int xMax = Position.x + 1;
            int yMin = Position.y - 1;
            int yMax = Position.y + 1;
            GameGrid grid = CurrentZoneInfo.grid;
            for (int x = xMin; x <= xMax; x++)
                for (int y = yMin; y <= yMax; y++)
                {
                    if ((x == Position.x && y == Position.y) || !grid.IsWithinBounds(x, y))
                        continue;
                    Cell cell = grid[x, y];
                    if (!cell.Contains(out Obstacle obstacle) || !obstacle.IsImpassable)
                        emptyPositions.Add(new Vector2Int(x, y));
                }
            if (emptyPositions.Count == 0)
                return false;
            position = Choose.Random(emptyPositions);
            return true;
        }

        protected override bool OnAIBeforeAttack(AIBeforeAttackEvent e)
        {
            if (TryUseAbility(e))
                return false;
            return base.OnAIBeforeAttack(e);
        }
    }
}
