using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TCD.Cinematics;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.UI;
using TCD.UI.Notifications;
using TCD.Zones;
using TCD.Zones.Dungeons;

namespace TCD
{
    [ContainsConsoleCommand]
    public class TCDGame : MonoBehaviour
    {
        public static readonly Thread thread = Thread.CurrentThread;

        private static GameSession session;

#if UNITY_WEBPLAYER
        private const string QUIT_URL = "http://google.com";
#endif
        [SerializeField] private GameObject startupScreen;

        [Header("Startup Options")]
        [SerializeField] private bool quickStart;
        [SerializeField] private string quickStartZone;
        [SerializeField] private bool suppressCrashNotification;
        [SerializeField] private string defaultCampaignName;
        [SerializeField] private string[] testVars;

        private bool lastSessionCrashed;

        public static GameSession Session => session;

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

            if (!quickStart)
                ViewManager.Open("Main Menu");
            else
                QuickStart(quickStartZone);    

            //ViewManager.Open("Object Editor v1");

            if (lastSessionCrashed && !suppressCrashNotification)
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

        private static void QuickStart(string zone)
        {
            Embark.SetChosenClass(Assets.Get<Class>("Turkinopoulos"));
            StartNewGame(zone);
        }
        
        [ConsoleCommand("newgame")]
        public static void StartNewGame(string zone = null)
        {
            session = new GameSession();

            DebugLogger.Log("New game started!");
            ViewManager.Close("Dev Console");
            Instantiate(Assets.Get<GameObject>("Over Screen Fade"), ParentManager.OverScreen);
            GameResetter.ResetGame();
            TCDGame game = ServiceLocator.Get<TCDGame>();

            if (!string.IsNullOrEmpty(zone))
            {
                ZoneGeneratorManager zoneGenerator = ServiceLocator.Get<ZoneGeneratorManager>();
                zoneGenerator.GenerateZone(ZoneFactory.BuildFromBlueprint(zone));
            }
            else
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
