using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Texts;

namespace TCD.Inputs.Actions
{
    public class PlayerActionManager : MonoBehaviour
    {
        public List<PlayerAction> actions = new List<PlayerAction>();
        public Dictionary<PlayerAction, KeyCommand> commands = 
            new Dictionary<PlayerAction, KeyCommand>();
        public PlayerAction currentAction;
        public int doNotActivateActionForFrames;

        private Coroutine currentActionRoutine;

#if UNITY_EDITOR
        [SerializeField] private List<string> actionNames = new List<string>();
#endif

        private void Awake()
        {
            KeyEventManager.Subscribe(InputGroup.Gameplay, KeyCommand.Cancel, KeyState.PressedThisFrame, e => { CancelCurrentAction(); });

            Add(new Interact(), KeyCommand.Interact);
            Add(new InteractAdvanced(), KeyCommand.InteractAdvanced);
            Add(new Look(), KeyCommand.Look);
        }

        private void Update()
        {
            if (doNotActivateActionForFrames > 0)
                doNotActivateActionForFrames--;
        }

        public void Add(PlayerAction action, KeyCommand command)
        {
            if (!actions.Contains(action))
            {
                actions.Add(action);
                commands.Add(action, command);
                KeyEventManager.Subscribe(InputGroup.Gameplay, command, KeyState.PressedThisFrame, e => { TryStartAction(action); });
#if UNITY_EDITOR
                actionNames.Add(action.Name);
#endif
            }
        }

        public void ChangeCommand(string actionName, KeyCommand newCommand)
        {
            PlayerAction action = Get(actionName);
            if (action != null)
            {
                Remove(actionName);
                Add(action, newCommand);
            }
        }

        public void Remove(string actionName)
        {
            PlayerAction action = Get(actionName);
            if (action != null)
            {
                actions.Remove(action);
                KeyCommand command = commands[action];
                KeyEventManager.Unsubscribe(InputGroup.Gameplay, command, KeyState.PressedThisFrame, e => { TryStartAction(action); });
                commands.Remove(action);
#if UNITY_EDITOR
                actionNames.Remove(action.Name);
#endif
            }
        }
        
        private PlayerAction Get(string actionName)
        {
            foreach (PlayerAction action in actions)
            {
                if (action.Name == actionName)
                    return action;
            }
            return null;
        }

        public void TryStartAction(string actionName, bool forced = false)
        {
            if (!KeyEventManager.GetInputGroupEnabled(InputGroup.Gameplay) || doNotActivateActionForFrames > 0)
                return;
            PlayerAction action = Get(actionName);
            if (action != null)
                TryStartAction(action, forced);
        }

        public void TryStartAction(PlayerAction action, bool forced = false)
        {
            if ((!KeyEventManager.GetInputGroupEnabled(InputGroup.Gameplay) || doNotActivateActionForFrames > 0) && !forced)
                return;
            if (currentAction != null)
                CancelCurrentAction();
            currentAction = action;
            currentAction.Start();
            if (currentAction != null)
            {
                MovementInterpreter.movementMode = currentAction.MovementMode;
                EventManager.Send(new PlayerActionStartedEvent(action));
                DebugLogger.Log($"Player has started action '{action.Name}'.");
            }
        }

        public void CancelCurrentAction()
        {
            if (currentAction == null)
                return;
            StopAllCoroutines();
            DebugLogger.Log($"Player has cancelled action '{currentAction.Name}'.");
            OnActionEnded();
        }

        private void OnActionEnded()
        {
            EventManager.Send(new PlayerActionEndedEvent());
            MovementInterpreter.movementMode = MovementMode.Free;
            MainCursor mainCursor = ServiceLocator.Get<MainCursor>();
            mainCursor.CenterOnPlayer();
            currentAction.End();
            currentAction = null;
        }

        public void OnCell(Cell cell)
        {
            if (currentAction == null || cell == null)
            {
                CancelCurrentAction();
                return;
            }
            this.EnsureCoroutineStopped(ref currentActionRoutine);
            int distance = Mathf.FloorToInt(Vector2Int.Distance(cell.Position, PlayerInfo.currentPlayer.cell.Position));
            if (distance > currentAction.GetRange())
            {
                FloatingTextHandler.Draw(PlayerInfo.currentPlayer.transform.position, "Too far!", Color.red);
                CancelCurrentAction();
                return;
            }
            if (cell.objects.Count == 1 && cell.objects[0].Parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
            {
                StartCoroutine(StartActionCoroutine(currentAction.OnObject(cell.objects[0])));
                DebugLogger.Log($"Player has used action '{currentAction.Name}' on object @{cell.Position}.");
            }
            else
            {
                StartCoroutine(StartActionCoroutine(currentAction.OnCell(cell)));
                DebugLogger.Log($"Player has used action '{currentAction.Name}' on cell @{cell.Position}.");
            }
            EventManager.Send(new PlayerActionPerformEvent());
        }

        private IEnumerator StartActionCoroutine(IEnumerator routine)
        {
            yield return currentActionRoutine = StartCoroutine(routine);
            OnActionEnded();
        }
    }
}
