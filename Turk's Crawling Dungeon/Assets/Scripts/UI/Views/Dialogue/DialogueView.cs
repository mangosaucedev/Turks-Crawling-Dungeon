using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Cinematics.Dialogues;

namespace TCD.UI
{
    public class DialogueView : MonoBehaviour
    {
        private const float SCROLL_DOWN_DELAY = 0.09f;
        private const int MAX_RESPONSES = 10;

        public static List<ViewButton> currentResponseButtons = new List<ViewButton>();

        private static string[] responseKeys = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

        [SerializeField] private Image portraitImage;
        [SerializeField] private Transform contentParent;
        [SerializeField] private Scrollbar scrollbar;

        private Dialogue currentNode;
        private WaitForSecondsRealtime scrollDownWait = new WaitForSecondsRealtime(SCROLL_DOWN_DELAY);

        public void DisplayDialogue(Dialogue node)
        {
            EventManager.Send(new BeforeDialogueUpdatedEvent());

            portraitImage.sprite = node.Speaker.Sprite;

            foreach (var button in currentResponseButtons)
            {
                if (button)
                    button.interactable = false;
            }
            currentResponseButtons.Clear();

            currentNode = node;
            GameObject prefab = Assets.Get<GameObject>("Dialogue Node");
            GameObject gameObject = Instantiate(prefab, contentParent);

            GoTo next = node.transitions.GetTransition();
            if (next == null)
            {
                if (node.choices.Count > 0)
                    CreateChoiceButtons();
                else
                    CreateEndConversationButton();
            }
            else
                CreateContinueButton();

            DialogueNodeDisplay nodeDisplay = gameObject.GetComponent<DialogueNodeDisplay>();
            nodeDisplay.DisplayDialogue(node);

            StopAllCoroutines();
            StartCoroutine(FinishDisplayRoutine());
        }

        private void CreateEndConversationButton()
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Continue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.SetText("[End conversation.]");
            viewButton.onClick.AddListener(currentResponseButtons.Clear);
            viewButton.onClick.AddListener(DialogueHandler.EndDialogue);
            currentResponseButtons.Add(viewButton);
        }

        private void CreateChoiceButtons()
        {
            int count = Mathf.Min(currentNode.choices.Count, MAX_RESPONSES);
            for (int i = 0; i < count; i++)
            { 
                Choice choice = currentNode.choices.choices[i];
                CreateChoiceButton(choice, i);
            }
        }

        private ViewButton CreateChoiceButton(Choice choice, int index)
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.key = responseKeys[index];
            viewButton.SetText(choice.text.ToString());
            
            GoTo transition = choice.transitions.GetTransition();
            if (transition == null)
            {
                viewButton.onClick.AddListener(() => DialogueHandler.EndDialogue());
                viewButton.onClick.AddListener(() => DebugLogger.Log("Response has no Go To Node; ending dialogue."));
            }
            else
            {
                viewButton.onClick.AddListener(() => DialogueHandler.GoToDialogueNode(transition.node));
                viewButton.onClick.AddListener(() => DebugLogger.Log("Response going to node " + transition.node));
            }
            currentResponseButtons.Add(viewButton);
            return viewButton;
        }

        private void CreateContinueButton()
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Continue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.SetText("[Continue.]");
            viewButton.onClick.AddListener(() => Destroy(gameObject));
            viewButton.onClick.AddListener(() => DialogueHandler.GoToDialogueNode(currentNode.GetTransition()));
            currentResponseButtons.Add(viewButton);
        }

        private IEnumerator FinishDisplayRoutine()
        {
            yield return null;
            DebugLogger.Log("Disabling dialogue response buttons!");
            currentResponseButtons.ForEach(b => b.interactable = false);
            yield return scrollDownWait;
            scrollbar.value = 0f;
        }
    }
}
