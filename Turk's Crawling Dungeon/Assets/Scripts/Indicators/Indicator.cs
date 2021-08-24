using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Indicators
{
    public abstract class Indicator : MonoBehaviour
    { 
        public Vector2Int startPosition;
        public GameObject targetObject;
        public Vector2Int targetPosition;
        public int range;
        public bool blockedByObstacles;

        private List<IndicatorSprite> indicatorSprites = new List<IndicatorSprite>();
        private int lockMouseMovementForFrames;

        public virtual void Start()
        {
            UpdateIndicator();
        }

        protected virtual void OnEnable()
        {
            EventManager.Listen<MouseEnterCellEvent>(this, OnMouseEnterCell);
            EventManager.Listen<CursorMovedEvent>(this, OnCursorMoved);
        }

        protected virtual void OnDisable()
        {
            EventManager.StopListening<MouseEnterCellEvent>(this);
            EventManager.StopListening<CursorMovedEvent>(this);
            ReturnIndicatorSpritesToPool();
        }

        private void Update()
        {
            if (lockMouseMovementForFrames > 0)
                lockMouseMovementForFrames--;
        }

        protected virtual void OnMouseEnterCell(MouseEnterCellEvent e)
        {
            if (lockMouseMovementForFrames <= 0)
                UpdateIndicator();
            lockMouseMovementForFrames = 0;
        }

        protected virtual void OnCursorMoved(CursorMovedEvent e)
        {
            lockMouseMovementForFrames = 2;
            UpdateIndicator();
        }

        protected virtual void UpdateIndicator()
        {
            ReturnIndicatorSpritesToPool();
        }

        protected virtual void ReturnIndicatorSpritesToPool()
        {
            foreach (IndicatorSprite indicatorSprite in indicatorSprites)
                indicatorSprite.ReturnToPool();
            indicatorSprites.Clear();
        }

        protected void IndicateTile(Vector2Int position, Sprite sprite)
        {
            GameGrid grid = CurrentZoneInfo.grid;
            if (!grid.IsWithinBounds(position))
                return;
            IndicatorSprite indicatorSprite = IndicatorSprite.Draw(position,  sprite);
            indicatorSprites.Add(indicatorSprite);
        }
    }
}
