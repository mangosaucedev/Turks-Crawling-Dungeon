using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Notifications
{
    public static class NotificationHandler
    {
        public static string currentTitle;
        public static string currentMessage;

        public static void Notify(string title, string message)
        {
            currentTitle = title;
            currentMessage = message;
            ViewManager.Open("Notification View");
        }
    }
}
