using System.Collections;
using System.Collections.Generic;
using TCD.Graphics.Indicators;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.TimeManagement;
using UnityEngine;

namespace TCD.Inputs.Actions
{
    public class Throw : PlayerAction
    {
        private BaseObject thrownObject;

        public override string Name => "Throw";

        public override MovementMode MovementMode => MovementMode.Cursor;

        private int Range
        {
            get
            {
                if (Throwable)
                    return Throwable.Range;
                return 0;
            }
        }

        private Throwable Throwable => thrownObject.parts.Get<Throwable>();

        public Throw(BaseObject thrownObject)
        {
            this.thrownObject = thrownObject;
        }

        public override void Start()
        {
            base.Start();
            IndicatorHandler.ShowIndicator("Player To Cursor Indicator", GetRange(), true);
        }

        public override void End()
        {
            base.End();
            IndicatorHandler.HideIndicator();
        }

        public override int GetRange() => Range;

        public override IEnumerator OnCell(Cell cell)
        {
            ThrowObject(cell.Position);
            yield break;
        }

        public override IEnumerator OnObject(BaseObject target)
        {  
            ThrowObject(target.cell.Position);
            yield break;
        }

        private void ThrowObject(Vector2Int position)
        {
            Throwable.Throw(PlayerInfo.currentPlayer.cell.Position, position);
            TimeScheduler.Tick(TimeInfo.TIME_PER_STANDARD_TURN);
        }  
    }
}
