using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class TalentTreeRowDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject talentButtonPrefab;

        public void BuildTalent(Talent talent)
        {
            GameObject talentButtonObject = Instantiate(talentButtonPrefab, transform);
            ITalentButton talentButton = talentButtonObject.GetComponent<ITalentButton>();
            talentButton.View = GetComponentInParent<TalentView>();
            talentButton.BuildTalent(talent);
        }
    }
}
