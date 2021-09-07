using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace TCD.Zones.Utilities
{
    public abstract class SeededGrid<T> : TGrid<T>
    {
        protected Random Random => RandomInfo.Random;
        
        public SeededGrid(int width, int height) : base(width, height)
        {

        }

        protected abstract void Fill();

        protected abstract void FillCell(int x, int y);
    }
}
