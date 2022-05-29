using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.TimeManagement;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Door : Part
    {
        [SerializeField] private bool isOpen;
        [SerializeField] private bool isLocked;
        [SerializeField] private bool canClose;
        [SerializeField] private string openSprite;
        [SerializeField] private string closedSprite;
        private Sprite savedClosedSprite;
        private Sprite savedOpenSprite;

        public override string Name => "Door";

        public bool IsOpen
        {
            get => isOpen;
            set => isOpen = value;
        }

        public bool IsLocked
        {
            get => isLocked;
            set => isLocked = value;
        }

        public bool CanClose
        {
            get => canClose;
            set => canClose = value;
        }

        public string OpenSprite
        {
            get => openSprite;
            set => openSprite = value;
        }

        public string ClosedSprite
        {
            get => closedSprite;
            set => closedSprite = value;
        }

        protected override void Start()
        {
            base.Start();
            ClosedSprite = parent.Parts.Get<Render>().Sprite;
            savedClosedSprite = parent?.SpriteRenderer?.sprite;
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            if (!IsOpen)
                e.interactions.Add(new Interaction("Open Door", OnPlayerOpen, true));
            else
                e.interactions.Add(new Interaction("Close Door", OnPlayerClose, true));
        }

        private void OnPlayerOpen()
        {
            if (!isOpen)
            {
                ActionScheduler.EnqueueAction(PlayerInfo.currentPlayer, OnOpen);
                TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN);
            }
        }

        private void OnOpen()
        {
            if (isOpen)
                return;
            isOpen = true;
            if (!savedOpenSprite)
                savedOpenSprite = Assets.Get<Sprite>(OpenSprite);
            parent.Parts.Get<Render>().SetSprite(OpenSprite);
            FireEvent(parent, new DoorOpenedEvent());
        }

        private void OnPlayerClose()
        {
            if (isOpen)
            {
                ActionScheduler.EnqueueAction(PlayerInfo.currentPlayer, OnClose);
                TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN);
            }
        }

        private void OnClose()
        {
            if (!isOpen)
                return;
            isOpen = false;
            parent.Parts.Get<Render>().SetSprite(ClosedSprite);
            FireEvent(parent, new DoorClosedEvent());
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == CanEnterCellEvent.id)
                OnCanEnterCell(e);
            if (e.Id == BeforeEnterCellEvent.id)
                OnOpen();
            return base.HandleEvent(e);
        }

        private void OnCanEnterCell(LocalEvent e)
        {
            if (isOpen || !isLocked)
            {
                CanEnterCellEvent canEnterCellEvent =
                    (CanEnterCellEvent)e;
                canEnterCellEvent.exceptions.Add(parent);
            }
        }
    }
}
