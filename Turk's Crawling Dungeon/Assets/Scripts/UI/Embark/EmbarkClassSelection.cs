using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            classes = Assets.FindAll<Class>();
            foreach (Class c in classes)
                dropdown.options.Add(new Dropdown.OptionData(c.name));
        }

        public void OnClassSelected(int classIndex)
        {
            Class c = classes[classIndex];
            Embark.SetChosenClass(c);
            description.text = c.description;
            if (!nextButton.activeInHierarchy)
                nextButton.SetActive(true);
        }
    }
}
