using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TCD.Cinematics;
using TCD.UI;
using TCD.UI.Notifications;
using TCD.Zones;
using TCD.Zones.Dungeons;

namespace TCD
{
    [ContainsConsoleCommand]
    public class TCDGame : MonoBehaviour
    {
#if UNITY_WEBPLAYER
        private const string QUIT_URL = "http://google.com";
#endif
        [SerializeField] private GameObject startupScreen;

        [Header("Startup Options")]
        [SerializeField] private string defaultCampaignName;
        [SerializeField] private string[] testVars;

        private bool lastSessionCrashed;

        private void Awake()
        {
            AssemblyInfo.executingAssemblies.Add(Assembly.GetExecutingAssembly());

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

            foreach (string var in testVars)
                GlobalVars.Set(var, true);
        }

        private IEnumerator Start()
        {
            DebugCrashHandler.CreateCrashFile();

            startupScreen = Instantiate(startupScreen, ParentManager.Canvas);
            yield return GameStartup.StartGame();
            Destroy(startupScreen);

            ViewManager.Open("Main Menu");
            //ViewManager.Open("Object Editor v1");

            if (lastSessionCrashed)
            {
                yield return NotificationHandler.WaitForNotificationToEnd();
                NotificationHandler.Notify(
                    "Last Session Failed To Close",
                    "Your last game session failed to close properly. This may have been caused by a " +
                    "crash. Be more careful next time!");
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        #region Console Commands

        [ConsoleCommand("set")]
        public static void CommandGlobalVarSet(string var, string value) => 
            GlobalVars.Set(var, value);

        [ConsoleCommand("get")]
        public static void CommandGlobalVarGet(string var) =>
            DebugLogger.Log(GlobalVars.Get(var).ToString());

        #endregion

        [ConsoleCommand("newgame")]
        public static void StartNewGame()
        {
            DebugLogger.Log("New game started!");
            ViewManager.Close("Dev Console");
            Instantiate(Assets.Get<GameObject>("Over Screen Fade"), ParentManager.OverScreen);
            GameResetter.ResetGame();
            TCDGame game = ServiceLocator.Get<TCDGame>();
            ZoneResetter.ResetZone(true);
            //CampaignHandler.StartCampaign(game.defaultCampaignName);
        }

        // TODO: REPLACE THIS WITH ENDING?
        public static void WinGame()
        {

        }

        [ConsoleCommand("exit")]
        [ConsoleCommand("quit")]
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
