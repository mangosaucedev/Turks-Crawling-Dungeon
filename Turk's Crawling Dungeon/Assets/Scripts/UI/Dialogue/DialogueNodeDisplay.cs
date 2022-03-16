using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TCD.Cinematics.Dialogues;
using TCD.Inputs;
using TCD.Texts;

namespace TCD.UI
{
    public class DialogueNodeDisplay : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private VerticalLayoutGroup layoutGroup;

        private Dialogue currentNode;
        private int index;
        private WaitForSecondsRealtime printWait;
        private bool isPrinting;
        private bool canSkipPrinting;
        private string fullText;
        private StringBuilder stringBuilder = new StringBuilder();
        private RectTransform parentRectTransform;

        private RectTransform ParentRectTransform
        {
            get
            {
                if (!parentRectTransform)
                    parentRectTransform = (RectTransform) transform.parent;
                return parentRectTransform;
             }
        }

        private void OnEnable()
        {
            EventManager.Listen<BeforeDialogueUpdatedEvent>(this, OnBeforeDialogueUpdated);
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            EventManager.StopListening<BeforeDialogueUpdatedEvent>(this);
            EventManager.StopListening<KeyEvent>(this);
        }

        private void OnBeforeDialogueUpdated(BeforeDialogueUpdatedEvent e) 
        {
            if (FinishPrinting())
                DebugLogger.Log($"Dialogue display skipping print: {e.Name} event!");
        }
        private bool FinishPrinting()
        {
            if (isPrinting && canSkipPrinting)
            {
                isPrinting = false;
                StopAllCoroutines();
                StartCoroutine(EnableResponseButtonsRoutine());
                text.text = $"{GetSpeakerName()} - {fullText}";
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) layoutGroup.transform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(ParentRectTransform);
                return true;
            }
            return false;
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.command == KeyCommand.Interact && 
                e.context.state == KeyState.PressedThisFrame &&
                FinishPrinting())
                DebugLogger.Log("Dialogue display skipping print: Interact button pressed!");
        }

        public void DisplayDialogue(Dialogue node) =>
            StartCoroutine(DisplayDialogueRoutine(node));


        public IEnumerator DisplayDialogueRoutine(Dialogue node)
        {
            currentNode = node;
            isPrinting = true;
            printWait = new WaitForSecondsRealtime(0.05f);
            fullText = new GameText(node.text);
            while (index < fullText.Length)
            {
                //DialogueView.currentResponseButtons.ForEach(b => DebugLogger.Log(
                //    b.name + " interactable: " + b.interactable + " isInteractable: " + b.IsInteractable()));

                index++;
                
                stringBuilder.Clear();
                stringBuilder.Append(GetSpeakerName());
                stringBuilder.Append(" - ");
                stringBuilder.Append(currentNode.text.ToString().Substring(0, index));
                text.text = stringBuilder.ToString();

                if (index > 1 && !canSkipPrinting)
                    canSkipPrinting = true;

                yield return printWait;
            }
            FinishPrinting();
        }

        private string GetSpeakerName() => 
            $"<color=#{ColorUtility.ToHtmlStringRGBA(currentNode.Speaker.Color)}>{currentNode.Speaker.displayName}</color>";

        private IEnumerator EnableResponseButtonsRoutine()
        {
            yield return null;
            DebugLogger.Log("Enabling dialogue response buttons!");
            DialogueView.currentResponseButtons.ForEach(b => b.interactable = true);
            ViewManager manager = ServiceLocator.Get<ViewManager>();
            manager.SelectTopLeftSelectable();
        }
    }
}
