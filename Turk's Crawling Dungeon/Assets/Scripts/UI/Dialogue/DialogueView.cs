using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Cinematics.Dialogue;

namespace TCD.UI
{
    public class DialogueView : MonoBehaviour
    {
        private const float SCROLL_DOWN_DELAY = 0.1f;
        private const int MAX_RESPONSES = 10;

        private static string[] responseKeys = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

        [SerializeField] private Image portraitImage;
        [SerializeField] private Transform contentParent;
        [SerializeField] private Scrollbar scrollbar;

        private DialogueNode currentNode;
        private List<ViewButton> currentResponseButtons = new List<ViewButton>();
        private WaitForSecondsRealtime scrollDownWait = new WaitForSecondsRealtime(SCROLL_DOWN_DELAY);

        public void DisplayDialogue(DialogueNode node)
        {
            EventManager.Send(new BeforeDialogueUpdatedEvent());

            portraitImage.sprite = node.speaker.portrait;

            foreach (ViewButton responsebutton in currentResponseButtons)
                responsebutton.interactable = false;
            currentResponseButtons.Clear();

            currentNode = node;
            GameObject prefab = Assets.Get<GameObject>("Dialogue Node");
            GameObject gameObject = Instantiate(prefab, contentParent);
            DialogueNodeDisplay nodeDisplay = gameObject.GetComponent<DialogueNodeDisplay>();
            nodeDisplay.DisplayDialogue(node);

            if (string.IsNullOrEmpty(node.goToNodeName))
            {
                if (node.responses.Count > 0)
                    CreateResponseButtons();
                else
                    CreateEndConversationButton();
            }
            else
                CreateContinueButton();

            StopAllCoroutines();
            StartCoroutine(ScrollDownRoutine());
        }

        private void CreateEndConversationButton()
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Continue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.SetText("[End conversation.]");
            viewButton.onClick.AddListener(DialogueHandler.EndDialogue);
        }

        private void CreateResponseButtons()
        {
            int count = Mathf.Min(currentNode.responses.Count, MAX_RESPONSES);
            for (int i = 0; i < count; i++)
            { 
                DialogueResponse response = currentNode.responses[i];
                ViewButton button = CreateResponseButton(response, i);
                currentResponseButtons.Add(button);
            }
        }

        private ViewButton CreateResponseButton(DialogueResponse response, int index)
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.key = responseKeys[index];
            viewButton.SetText(response.text.ToString());
            if (!response.HasGoToNode)
            {
                viewButton.onClick.AddListener(() => DialogueHandler.EndDialogue());
                viewButton.onClick.AddListener(() => DebugLogger.Log("Response has no Go To Node (" + response.goToNodeName + "); ending dialogue."));
            }
            else
            {
                viewButton.onClick.AddListener(() => DialogueHandler.GoToDialogueNode(response.goToNodeName));
                viewButton.onClick.AddListener(() => DebugLogger.Log("Response going to node " + response.goToNodeName));
            }
            return viewButton;
        }

        private void CreateContinueButton()
        {
            GameObject prefab = Assets.Get<GameObject>("Dialogue Continue Button");
            GameObject gameObject = Instantiate(prefab, contentParent);
            ViewButton viewButton = gameObject.GetComponent<ViewButton>();
            viewButton.SetText("[Continue.]");
            viewButton.onClick.AddListener(() => Destroy(gameObject));
            viewButton.onClick.AddListener(() => DialogueHandler.GoToDialogueNode(currentNode.goToNodeName));
        }

        private IEnumerator ScrollDownRoutine()
        {
            yield return scrollDownWait;
            scrollbar.value = 0f;
        }
    }
}
