using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class TalentView : MonoBehaviour
    {
        public TalentDescriptionStyle style = TalentDescriptionStyle.TalentView;

        [SerializeField] private bool buildOnStart;
        [SerializeField] private Transform content;
        [SerializeField] private Text talentPoints;
        [SerializeField] private Text title;
        [SerializeField] private Text description;
        [SerializeField] private GameObject talentTreePrefab;

        private List<GameObject> talentTreeObjects = new List<GameObject>();

        private void Start()
        {
            description.text = "";
            if (buildOnStart)
                BuildTalents();
            UpdateTalentPointCount();
        }

        private void OnEnable()
        {
            EventManager.Listen<EmbarkTalentPointModifiedEvent>(this, OnEmbarkTalentPointModified);
        }

        private void OnDisable()
        {
            EventManager.StopListening<EmbarkTalentPointModifiedEvent>(this);
        }

        private void OnEmbarkTalentPointModified(EmbarkTalentPointModifiedEvent e)
        {
            if (e.level == 0)
            {
                UpdateDescription(null, 0);
                return;
            }
            UpdateDescription(TalentUtility.GetTalentInstance(e.type), e.level);
        }

        public void BuildTalents()
        {
            for (int i = talentTreeObjects.Count - 1; i >= 0; i--)
            {
                GameObject gameObject = talentTreeObjects[i];
                Destroy(gameObject);
            }
            talentTreeObjects.Clear();

            Class c = PlayerInfo.currentClass;
            foreach (TalentTree talentTree in c.TalentTrees)
                BuildTalentTree(talentTree);
        }

        private void BuildTalentTree(TalentTree talentTree)
        {
            GameObject talentTreeObject = Instantiate(talentTreePrefab, content);
            TalentTreeDisplay talentTreeDisplay = talentTreeObject.GetComponent<TalentTreeDisplay>();
            talentTreeDisplay.BuildTalentTree(talentTree);
            talentTreeObjects.Add(talentTreeObject);
        }

        public void UpdateTalentPointCount()
        {
            talentPoints.text = $"{PlayerInfo.talentPoints} points to spend on talents";
        }

        public void UpdateDescription(Talent talent, int level)
        {
            if (!talent)
            {
                title.text = "";
                description.text = "";
                return;
            }
            title.text = TalentUtility.BuildTalentTitle(talent);
            description.text = TalentUtility.BuildTalentDescription(talent, level, style);
        }
    }
}
