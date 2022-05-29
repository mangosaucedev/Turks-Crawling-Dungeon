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
        CameraResetZoom,
        FadeColor,
        FadeColorHex,
        StartDialogue,
        StartMonologue,
        ClearMonologue,
        StartCinematic,
        FireTrigger
    }
}
