using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Obstacle : Part
    {
        [SerializeField] private bool isImpassable;
        [SerializeField] private int difficulty;
        [SerializeField] private bool occludesLineOfSight;

        public bool IsImpassable
        {
            get => isImpassable;
            set => isImpassable = value;
        }

        public int Difficulty
        {
            get => difficulty;
            set => difficulty = value;
        }

        public bool OccludesLineOfSight
        {
            get => occludesLineOfSight;
            set => occludesLineOfSight = value;
        }

        public override string Name => "Obstacle";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == CanEnterCellEvent.id)
                OnCanEnterCell(e);
            if (e.Id == DoorOpenedEvent.id)
            {
                IsImpassable = false;
                OccludesLineOfSight = false;
            }
            if (e.Id == DoorClosedEvent.id)
            {
                IsImpassable = true;
                OccludesLineOfSight = true;
            }
            if (e.Id == GetDescriptionEvent.id)
                OnGetDescription(e);
            return base.HandleEvent(e);
        }

        private void OnCanEnterCell(LocalEvent e)
        {
            if (IsImpassable)
            {
                CanEnterCellEvent canEnterCellEvent =
                    (CanEnterCellEvent)e;
                canEnterCellEvent.obstacles.Add(parent);
            }
        }

        private void OnGetDescription(LocalEvent e)
        {
            GetDescriptionEvent getDescriptionEvent = (GetDescriptionEvent) e;
            if (IsImpassable)
                getDescriptionEvent.AddToSuffix("<i>Impassable.</i>");
            else
                getDescriptionEvent.AddToSuffix("<i>Difficult to navigate.</i>");
        }
    }
}
