using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
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
    }
}
