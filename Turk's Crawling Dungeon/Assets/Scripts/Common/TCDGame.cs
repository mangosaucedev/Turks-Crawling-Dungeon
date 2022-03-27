using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TCD.UI;
using TCD.UI.Notifications;
using TCD.Zones.Dungeons;

namespace TCD
{
    [ContainsConsoleCommand]
    public class TCDGame : MonoBehaviour
    {
#if UNITY_WEBPLAYER
        private const string QUIT_URL = "http://google.com";
#endif

        public string desiredState;

        [SerializeField] private GameObject startupScreen;

        [Header("Startup Options")]
        [SerializeField] private string defaultCampaignName;
        [SerializeField] private string[] testVars;

#if UNITY_EDITOR
        [SerializeField] private string state;
#endif

        private StateMachine stateMachine;
        private bool lastSessionCrashed;

        public StateMachine StateMachine
        {
            get
            {
                if (stateMachine == null)
                    stateMachine = new StateMachine();
                return stateMachine;
            }
        }

        public string State
        {
            get => StateMachine.State;
            set => StateMachine.GoToState(value);
        }

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

            StateMachine.EnableLogging("Game State Machine");
            StateMachine.AddState(new Gameplay());
            StateMachine.AddState(new InCinematic());
            StateMachine.AddState(new InView());
            StateMachine.AddState(new InAction());
            StateMachine.AddState(new Loading(), true);

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

        private void Update()
        {
#if UNITY_EDITOR
            state = State;
#endif
            StateMachine?.Update();
        }

        private void FixedUpdate()
        {
            StateMachine?.FixedUpdate();
        }

        private void OnEnable()
        {
            EventManager.Listen<ViewOpenedEvent>(this, OnViewOpened);
            EventManager.Listen<ViewClosedEvent>(this, OnViewClosed);
            EventManager.Listen<PlayerActionPerformEvent>(this, OnPlayerActionPerform);
            EventManager.Listen<PlayerActionEndedEvent>(this, OnPlayerActionEnded);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            EventManager.StopListening<ViewOpenedEvent>(this);
            EventManager.StopListening<ViewClosedEvent>(this);
            EventManager.StopListening<PlayerActionPerformEvent>(this);
            EventManager.StopListening<PlayerActionEndedEvent>(this);
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
            ViewManager.Close("Dev Console");
            GameResetter.ResetGame();
            TCDGame game = ServiceLocator.Get<TCDGame>();
            game.desiredState = "Gameplay";
            CampaignHandler.StartCampaign(game.defaultCampaignName);
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

        #region Events

        private void OnViewOpened(ViewOpenedEvent e)
        {
            if (e.view.locksInput && State != "InView" && State != "Loading")
            {
                desiredState = State == "InAction" ? "Gameplay" : State;
                State = "InView";
            }
        }

        private void OnViewClosed(ViewClosedEvent e)
        {
            bool noViewActive = ViewManager.activeViews.Count == 0;
            bool activeViewDoesNotLockInput = noViewActive ? true : !ViewManager.TryFind(ViewManager.GetActiveView(), out ActiveView activeView) || !activeView.locksInput;
            if (State == "InView" && (noViewActive || activeViewDoesNotLockInput))
                State = desiredState;
        }

        private void OnPlayerActionPerform(PlayerActionPerformEvent e)
        {
            if (State == "InView" || State == "Loading")
                return;
            desiredState = State;
            State = "InAction";
        }

        private void OnPlayerActionEnded(PlayerActionEndedEvent e)
        {
            if (State == "InView" || State == "Loading")
                return;
            State = desiredState;
        }

        #endregion
    }
}
