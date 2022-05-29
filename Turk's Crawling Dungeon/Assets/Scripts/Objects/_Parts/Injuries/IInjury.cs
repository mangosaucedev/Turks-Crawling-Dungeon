using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public interface IInjury
    {
        Injuries Injuries { get; set;}

        void OnAcquire();

        void OnHealed();

        bool PassTime(int time);

        float GetSeverity();
    
        string GetName();

        string GetDescription();
    }
}
