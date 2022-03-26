using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD.Objects.Parts
{
    [Serializable]
    public abstract class Liquid : Part
    {
        private const int COLOR_CHANGE_DEPTH = 900;
        private const int WADE_DEPTH = 1000;
        private const int FLOW_DEPTH = 2500;
        private const int SWIM_DEPTH = 4000;
        private const int MAX_DEPTH = 6116;
        private const int FLOW_CELLS = 8;
        private const float FLOW_AMOUNT = 100f;

        public float depth;
        public bool isFlowing;

        [SerializeField] protected string liquidColor;
        [SerializeField] private Vector2Int tilePosition;
        [SerializeField] private List<Cell> unoccupiedAdjacentCells = new List<Cell>();

        public string LiquidColor
        {
            get => liquidColor;
            set => liquidColor = value;
        }

        public float Depth
        {
            get => depth;
            set => depth = value;
        }

        protected override void Start()
        {
            base.Start();
            Draw();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
            if (!hasStarted)
                return;
            Draw();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<AfterTurnTickEvent>(this);
            if (isApplicationQuitting)
                return;
            Erase();
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            if (Depth <= FLOW_DEPTH)
            {
                isFlowing = false;
                return;
            }
            float flowMultiplier = e.timeElapsed / TimeInfo.TIME_PER_STANDARD_TURN;
            if (FlowToAdjacentCells(flowMultiplier))
                isFlowing = true;
        }

        private bool FlowToAdjacentCells(float flowMultiplier)
        {
            GetUnoccupiedAdjacentCells();
            if (unoccupiedAdjacentCells.Count == 0)
                return false;
            flowMultiplier *= (float) FLOW_CELLS / unoccupiedAdjacentCells.Count;
            float maxFlowAmount = Mathf.Ceil(FLOW_AMOUNT * flowMultiplier);
            float minFlowAmount = Mathf.Ceil((Depth - FLOW_DEPTH) / unoccupiedAdjacentCells.Count);
            float flowAmount = Mathf.Min(minFlowAmount, maxFlowAmount);
            if (flowAmount == 0f)
                return false;
            foreach (Cell cell in unoccupiedAdjacentCells)
                FlowToCell(cell, flowAmount);
            float totalFlow = flowAmount * unoccupiedAdjacentCells.Count;
            AdjustDepth(-totalFlow);
            return true;
        }

        private void GetUnoccupiedAdjacentCells()
        {
            unoccupiedAdjacentCells.Clear();
            GameGrid grid = CurrentZoneInfo.grid;
            for (int xOffset = -1; xOffset <= 1; xOffset++)
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    Vector2Int offset = new Vector2Int(xOffset, yOffset);
                    Vector2Int checkPosition = Position + offset;
                    if ((xOffset == 0 && yOffset == 0) || !grid.IsWithinBounds(checkPosition))
                        continue;
                    Cell cell = grid[checkPosition];
                    bool isImpassable = cell.Contains(out Obstacle obstacle) && obstacle.IsImpassable;
                    if (isImpassable)
                        continue;
                    unoccupiedAdjacentCells.Add(cell);
                }
        }

        private void FlowToCell(Cell cell, float flowAmount)
        {
            if (cell.Contains(out Liquid liquid))
            {
                liquid.AdjustDepth(flowAmount);
                return;
            }
            Vector2Int position = cell.Position;
            BaseObject liquidObject = ObjectFactory.BuildFromBlueprint(parent.name, position);
            liquid = liquidObject.Parts.Get<Liquid>();
            liquid.SetDepth(flowAmount);
        }

        public void AdjustDepth(float amount) => SetDepth(Depth + amount);
        
        public void SetDepth(float amount)
        {
            Depth = Mathf.Clamp(amount, 0, MAX_DEPTH);
            if (Depth <= 0)
                parent.Destroy();
            else
                Draw();
        }

        private void Draw()
        {
            LiquidTilemapManager liquidTilemapManager =
                ServiceLocator.Get<LiquidTilemapManager>();
            liquidTilemapManager.Erase(Position);
            liquidTilemapManager.Draw(Position, GetColor());
            tilePosition = Position;
        }

        protected Color GetColor()
        {
            if (!ColorUtility.TryParseHtmlString(LiquidColor, out Color color))
                color = Color.white;
            if (Depth <= WADE_DEPTH)
                return color;
            else
            {
                int difference = Mathf.CeilToInt(Depth - COLOR_CHANGE_DEPTH);
                int max = MAX_DEPTH - COLOR_CHANGE_DEPTH;
                float colorShift = ((float) difference / max) * 0.8f;
                return Color.Lerp(color, Color.black, colorShift);
            }
        }

        private void Erase()
        {
            LiquidTilemapManager liquidTilemapManager =
                ServiceLocator.Get<LiquidTilemapManager>();
            liquidTilemapManager.Erase(tilePosition);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetDescriptionEvent.id)
                OnGetDescription((GetDescriptionEvent) (LocalEvent)e);
            if (e.Id == EnteredCellEvent.id)
                OnEnteredCell((EnteredCellEvent) (LocalEvent) e);
            if (e.Id == ExitedCellEvent.id)
                OnExitedCell((ExitedCellEvent) (LocalEvent)e);
            return base.HandleEvent(e);
        }

        protected virtual void OnGetDescription(GetDescriptionEvent e)
        {
            if (e.obj != parent)
                return;
            e.AddToSuffix($"{Depth} liters.");
            if (Depth >= SWIM_DEPTH)
                e.AddToSuffix("<color=blue>Very deep. You'll have to swim through this cell.</color>");
            else if (Depth >= WADE_DEPTH)
                e.AddToSuffix("<color=blue>Deep. You'll have to wade through this cell.</color>");
        }

        protected virtual void OnEnteredCell(EnteredCellEvent e)
        {
            BaseObject obj = e.obj;
            if (!obj.Parts.Get<Movement>() || !obj.Parts.TryGet(out Effects.Effects effects))
                return;
            if (Depth >= SWIM_DEPTH)
            {
                effects.AddEffect(new Swimming(), TimeInfo.TIME_PER_STANDARD_TURN * 999);
                return;
            }
            if (Depth >= WADE_DEPTH)
            {
                effects.AddEffect(new Wading(), TimeInfo.TIME_PER_STANDARD_TURN * 999);
                return;
            }
        }

        protected virtual void OnExitedCell(ExitedCellEvent e)
        {
            BaseObject obj = e.obj;
            if (!obj.Parts.TryGet(out Movement movement) || !obj.Parts.TryGet(out Effects.Effects effects))
                return;
            effects.RemoveEffect("Swimming");
            effects.RemoveEffect("Wading");
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Drink", OnDrink));
        }

        protected virtual void OnDrink()
        {
            AdjustDepth(-1);
        }
    }
}
