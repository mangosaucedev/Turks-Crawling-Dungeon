using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public static class ObjectCuller
    {
        private static int CULLED = -int.MaxValue;
        private static int ACTIVATION_DISTANCE = 42;

        public static List<BaseObject> objectsToCull = new List<BaseObject>();

        public static void PerformCulling()
        {   
            if (!PlayerInfo.currentPlayer)
                return;
            DeactivateObjectsBeyondActivationDistance();
            ActivateObjectsAtActivationDistance();
        }

        private static void DeactivateObjectsBeyondActivationDistance()
        {
            foreach (BaseObject obj in objectsToCull)
            {
                if (ObjectUtility.GetDistanceToObject(obj, PlayerInfo.currentPlayer) > ACTIVATION_DISTANCE)
                    obj.deactivator.AddDeactivatingCondition(CULLED);
            }
        }

        private static void ActivateObjectsAtActivationDistance()
        {
            foreach (BaseObject obj in objectsToCull)
            {
                if (ObjectUtility.GetDistanceToObject(obj, PlayerInfo.currentPlayer) <= ACTIVATION_DISTANCE)
                    obj.deactivator.RemoveDeactivatingCondition(CULLED);
            }
        }
    }
}
