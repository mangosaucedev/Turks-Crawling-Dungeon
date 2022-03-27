using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class TalentTreeDisplay : MonoBehaviour
    {
        private const int MAX_TALENTS_TO_ROW = 4;

        [SerializeField] private GameObject talentTreeRowPrefab;
        [SerializeField] private Text label;
        [SerializeField] private Image icon;
        [SerializeField] private Transform content;
        [SerializeField] private Sprite closedIcon;
        [SerializeField] private Sprite openIcon;

        public void BuildTalentTree(TalentTree talentTree)
        {
            label.text = talentTree.displayName;

            TalentTreeRowDisplay talentTreeRowDisplay = null;
            for (int i = 0; i < talentTree.Talents.Count; i++)
            {
                if (i % MAX_TALENTS_TO_ROW == 0)
                {
                    GameObject talentTreeRowObject = Instantiate(talentTreeRowPrefab, content);
                    talentTreeRowDisplay = talentTreeRowObject.GetComponent<TalentTreeRowDisplay>();
                }
                Talent talent = talentTree.Talents[i];
                talentTreeRowDisplay.BuildTalent(talent);
            }
        }

        public void OpenCloseContent()
        {
            if (content.gameObject.activeInHierarchy)
            {
                icon.sprite = closedIcon;
                content.gameObject.SetActive(false);
            }
            else
            {
                icon.sprite = openIcon;
                content.gameObject.SetActive(true);
            }
        }
    }
}
