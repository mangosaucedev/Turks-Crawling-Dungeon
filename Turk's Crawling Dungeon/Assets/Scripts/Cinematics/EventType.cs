using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public enum EventType 
    {
        Unknown,
        SetTarget,
        ResetTarget,
        WaitForSeconds,
        WaitForInput,
        WaitForViews,
        CameraMoveToTargetPosition,
        CameraLerpToTargetPosition,
        CameraLookAtTarget,
        FadeColor,
        FadeColorHex,
        StartDialogue,
        StartMonologue,
        ClearMonologue,
        FireTrigger
    }
}
