using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Visible : Part
    {
        [SerializeField] private bool isVisible;
        [SerializeField] private int visibilityRadius;

        public override string Name => "Visible";

        public int VisibilityRadius
        {
            get => visibilityRadius;
            set => visibilityRadius = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<AfterFOVUpdateEvent>(this, OnAfterFOVUpdate);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<AfterFOVUpdateEvent>(this);
        }

        private void OnAfterFOVUpdate(AfterFOVUpdateEvent e) => UpdateVisibility();

        private void UpdateVisibility()
        {
            isVisible = IsVisibleToPlayer();
            Render render = parent.Parts.Get<Render>();
            
            if (isVisible)
                render.EnableSprite();
            if (!isVisible)
                render.DisableSprite();
            
            FireVisibilityChangedEvent();
        }

        private void FireVisibilityChangedEvent()
        {
            VisibilityChangedEvent e = LocalEvent.Get<VisibilityChangedEvent>();
            e.visible = isVisible;
            FireEvent(parent, e);
        }

        public bool IsVisibleToPlayer() => (FieldOfView.IsVisible(Position) && 
                (VisibilityRadius == 0 || PlayerInfo.GetDistanceToPlayer(parent) <= VisibilityRadius));
        
    }
}
