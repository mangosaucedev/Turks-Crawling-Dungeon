using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TCD.Cinematics.Dialogues;
using TCD.Inputs;
using TCD.Texts;
using RedBlueGames.Tools.TextTyper;

namespace TCD.UI
{
    public class DialogueNodeDisplay : MonoBehaviour
    {
        private const float TYPE_DELAY = 0.05f;

        public TextTyper typer;

        [SerializeField] private Text text;
        [SerializeField] private VerticalLayoutGroup layoutGroup;

        private Dialogue currentNode;
        private int charactersPrinted;
        private bool canSkipPrinting;
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
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            EventManager.StopListening<KeyEvent>(this);
        }

        private void OnKey(KeyEvent e)
        {
            if (e.context.command == KeyCommand.Interact && 
                e.context.state == KeyState.PressedThisFrame &&
                canSkipPrinting)
                typer.Skip();
        }

        public void DisplayText(string text)
        {
            typer.TypeText(text);
            typer.Skip();
        }

        public void DisplayDialogue(Dialogue node)
        {
            currentNode = node;
            charactersPrinted = 0;
            typer.TypeText(GetSpeakerName() + " - " + new GameText(currentNode.text), TYPE_DELAY);
        }

        private string GetSpeakerName() => 
            $"<color=#{ColorUtility.ToHtmlStringRGBA(currentNode.Speaker.Color)}>{currentNode.Speaker.displayName}</color>";

        public void OnCharacterPrinted()
        {
            charactersPrinted++;
            if (charactersPrinted > 1 && !canSkipPrinting)
                canSkipPrinting = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutGroup.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(ParentRectTransform);
        }

        public void OnPrintComplete()
        {
            canSkipPrinting = false;
        }
    }
}
