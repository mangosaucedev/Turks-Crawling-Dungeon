using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;
using TCD.UI.Notifications;

namespace TCD.Cheats
{
    [ContainsConsoleCommand]
    public static class UICheats 
    {
        [ConsoleCommand("spam")]
        public static void NotificationSpam()
        {
            for (int i = 0; i < 6; i++)
                NotificationHandler.Notify(
                    Choose.Random("Hello", "Hi", "Notification", "Test", "Spam!", "Spam1", "'Sup?", "Groovy!"),
                    Choose.Random("Wassup", "What's poppin?", "Yo!", "Hiya...", "Waddup!", "Hey hey", "Howdy", "Le spam!1!"));
        }
    }
}
