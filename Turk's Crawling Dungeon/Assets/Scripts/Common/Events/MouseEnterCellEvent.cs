using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class MouseEnterCellEvent : Event
    {
        public Cell cell;

        public override string Name => "Mouse Enter Cell";

        public MouseEnterCellEvent(Cell cell) : base()
        {
            this.cell = cell;
        }
    }
}
