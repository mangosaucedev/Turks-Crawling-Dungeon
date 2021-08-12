using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class StatsException : PartException
    {
        public StatsException(Part part) : base(part)
        {

        }

        public StatsException(Part part, string message) : base(part, message)
        {

        }
    }
}
