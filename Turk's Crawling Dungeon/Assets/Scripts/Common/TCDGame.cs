using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TCD.IO.Serialization;
using TCD.UI;
using TCD.UI.Notifications;
using TCD.Zones.Dungeons;

namespace TCD
{
    public class TCDGame : MonoBehaviour
    { 
        [SerializeField] private string defaultCampaignName;
        [SerializeField] private GameObject startupScreen;

        private bool lastSessionCrashed;

        private void Awake()
        {
            lastSessionCrashed = DebugCrashHandler.CrashFilePersists();

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    DebugLogger.DumpToLog();
                    DebugCrashHandler.DeleteCrashFile();
                }
            };
#else
            Application.quitting += DebugLogger.DumpToLog;
            Application.quitting += DebugCrashHandler.DeleteCrashFile;
#endif
        }

        private IEnumerator Start()
        {
            DebugCrashHandler.CreateCrashFile();

            startupScreen = Instantiate(startupScreen, ParentManager.Canvas);

            yield return GameStartup.StartGame();

            Destroy(startupScreen);

            ViewManager.Open("Main Menu");

            if (lastSessionCrashed)
            {
                yield return NotificationHandler.WaitForNotificationToEnd();
                NotificationHandler.Notify(
                    "Last Session Failed To Close",
                    "Your last game session failed to close properly. This may have been caused by a " +
                    "crash. Be more careful next time!");
            }
        }

        public static void StartNewGame()
        {
            GameResetter.ResetGame();
            TCDGame game = ServiceLocator.Get<TCDGame>();
            CampaignHandler.StartCampaign(game.defaultCampaignName);
        }

        // TODO: REPLACE THIS WITH ENDING?
        public static void WinGame()
        {

        }

        public static void ExitToDesktop()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            //Application.Quit();
            DebugLogger.DumpToLog();
            DebugCrashHandler.DeleteCrashFile();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        }
    }
}
