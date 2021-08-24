using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MouseExitCellEvent : Event
    {
        public Cell cell;

        public override string Name => "Mouse Exit Cell";

        public MouseExitCellEvent(Cell cell) : base()
        {
            this.cell = cell;
        }
    }
}
