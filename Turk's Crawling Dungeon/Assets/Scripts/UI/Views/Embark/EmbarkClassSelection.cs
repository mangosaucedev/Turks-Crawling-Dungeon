using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;

namespace TCD.UI
{
    public class EmbarkClassSelection : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private Text description;

        private List<Class> classes = new List<Class>();

        private void Start()
        {
            List<Class> allClasses = Assets.FindAll<Class>();
            dropdown.options.Add(new Dropdown.OptionData(" "));
            foreach (Class c in allClasses)
            {
                if (c.unlisted)
                    continue;
                classes.Add(c);
                dropdown.options.Add(new Dropdown.OptionData(c.name));
            }
            dropdown.SetValueWithoutNotify(-1);
        }

        private void OnEnable()
        {
            dropdown.Select();
        }

        public void OnClassSelected(int classIndex)
        {
            classIndex -= 1;
            if (classIndex == -1)
            {
                if (nextButton.activeInHierarchy)
                    nextButton.SetActive(false);
                return;
            }
            Class c = classes[classIndex];
            Embark.SetChosenClass(c);
            RebuildPlayer();
            description.text = c.description;
            if (!nextButton.activeInHierarchy)
                nextButton.SetActive(true);
        }
        private void RebuildPlayer()
        {
            if (PlayerInfo.currentPlayer)
                Destroy(PlayerInfo.currentPlayer.gameObject);
            PlayerInfo.currentPlayer = ObjectFactory.BuildFromBlueprint("Player", Vector2Int.zero);
            PlayerInfo.currentPlayer.gameObject.SetActive(false);
            PlayerInfo.currentClass = Embark.ChosenClass;
        }
    }
}
