using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TCD.Cinematics.Dialogue;
using TCD.Inputs;

namespace TCD.UI
{
    public class DialogueNodeDisplay : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private VerticalLayoutGroup layoutGroup;

        private DialogueNode currentNode;
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
            DebugLogger.Log($"Dialogue display skipping print: {e.Name} event!");
            FinishPrinting();
        }
        private void FinishPrinting()
        {
            if (isPrinting && canSkipPrinting)
            {
                isPrinting = false;
                StopAllCoroutines();
                text.text = $"{GetSpeakerName()} - {fullText}";
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) layoutGroup.transform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(ParentRectTransform);
            }
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.command == KeyCommand.Interact && e.context.state == KeyState.PressedThisFrame && isPrinting)
            {
                DebugLogger.Log("Dialogue display skipping print: Interact button pressed!");
                FinishPrinting();
            }  
        }

        public void DisplayDialogue(DialogueNode node) =>
            StartCoroutine(DisplayDialogueRoutine(node));


        public IEnumerator DisplayDialogueRoutine(DialogueNode node)
        {
            currentNode = node;
            isPrinting = true;
            printWait = new WaitForSecondsRealtime(0.05f);
            fullText = currentNode.text.ToString();
            while (index < fullText.Length)
            {
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
            $"<color=#{ColorUtility.ToHtmlStringRGBA(currentNode.speaker.Color)}>{currentNode.speaker.displayName}</color>";
    }
}
