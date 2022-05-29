using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TCD.UI.Notifications;

namespace TCD
{
    public static class ExceptionHandler 
    {
        public static bool safeMode = true;

        public static void Handle(Exception e) =>
            HandleMessage(e.Message);

        public static void HandleMessage(string message)
        {
            if (Thread.CurrentThread != TCDGame.thread)
                return;
            DebugLogger.LogException(message);
            if (!safeMode)
                TCDGame.ExitToDesktop();
            else
                NotificationHandler.Notify("FATAL ERROR!", "A fatal error has occurred: \n" + message);
        }
    }
}
