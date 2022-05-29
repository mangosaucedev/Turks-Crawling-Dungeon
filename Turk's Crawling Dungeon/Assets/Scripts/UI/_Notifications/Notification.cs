using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Notifications
{
    public class Notification
    {
        public string title;
        public string message;
        public NotificationButton[] buttons; 

        public int Buttons
        {
            get
            {
                if (buttons == null)
                    return 0;
                return buttons.Length;
            }
        }

        public Notification(string title, string message, params NotificationButton[] buttons)
        {
            this.title = title;
            this.message = message;
            this.buttons = buttons;
        }
    }
}
