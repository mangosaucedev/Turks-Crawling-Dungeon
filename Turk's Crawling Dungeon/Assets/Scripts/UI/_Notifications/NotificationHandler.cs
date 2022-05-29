using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Notifications
{
    public static class NotificationHandler
    {
        private static Queue<Notification> notifications = new Queue<Notification>();
        private static WaitUntil waitUntilNotificationCleared;

        private static Notification Current
        {
            get
            {
                if (notifications.Count == 0)
                    return null;
                return notifications.Peek();
            }
        }

        static NotificationHandler()
        {
            waitUntilNotificationCleared = new WaitUntil(() => !IsNotificationOpen());
        }

        private static bool IsNotificationOpen() => 
            ViewManager.IsOpen("Notification")
            || ViewManager.IsOpen("Notification Buttons")
            || ViewManager.IsOpen("Notification Buttons Vertical");

        public static void Notify(string title, string message) => 
            Notify(title, message, null);

        public static void Notify(string title, string message, params NotificationButton[] buttons) =>
            Notify(new Notification(title, message, buttons));

        public static void Notify(Notification notification)
        {
            notifications.Enqueue(notification);
            if (!IsNotificationOpen())
                DisplayNotification();
        }

        private static void DisplayNotification()
        {
            if (Current == null)
                return;

            DebugLogger.Log("[Notification]: Displaying " + Current.title);

            string[] notifications = 
                new string[] { "Notification", "Notification Buttons", "Notification Buttons Vertical"};
            int index = 0;

            if (Current.Buttons > 0 && Current.Buttons < 3)
                index = 1;
            if (Current.Buttons >= 3)
                index = 2;
            string notification = notifications[index];

            foreach (string n in notifications)
                ViewManager.Close(n);

            ViewManager.Open(notification);

            NotificationView view = ServiceLocator.Get<NotificationView>();
            view.Display(NotificationHandler.notifications.Dequeue());
        }

        public static void DisplayNext()
        {
            if (notifications.Count == 0 || Current == null)
                return;
            DisplayNotification();
        }

        public static IEnumerator WaitForNotificationToEnd()
        {
            yield return waitUntilNotificationCleared;
        }
    }
}
