using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.UI
{
    public class ResourceBars : MonoBehaviour
    {
        private static WaitForSecondsRealtime wait = new WaitForSecondsRealtime(2f);

        [SerializeField] private Transform parent;
        
        private List<ResourceBar> resourceBars = new List<ResourceBar>();
        private (Resource resource, float displayPoint)[] resources;
        private bool showAltText;

        private BaseObject Player => PlayerInfo.currentPlayer;

        private Resources Resources => Player?.parts.Get<Resources>();

        private void Awake()
        {
            resources = new (Resource resource, float displayPoint)[]
            {
                (Resource.Hitpoints, 1),
                (Resource.Stamina, 0.95f),
                (Resource.Psi, 0.95f),
                (Resource.Oxygen, 0.95f),
                (Resource.Hunger, 0.5f),
                (Resource.Thirst, 0.5f),
                (Resource.Morale, 0.45f),
                (Resource.Stimulation, 0.45f)
            };
        }

        private void OnEnable()
        {
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);  
            EventManager.Listen<PlayerCreatedEvent>(this, OnPlayerCreated);  
            UpdateResourceBars();
            StartCoroutine(SwapTextRoutine());
        }

        private void OnDisable()
        {
            EventManager.StopListening<AfterTurnTickEvent>(this);
            EventManager.StopListening<PlayerCreatedEvent>(this);
            StopAllCoroutines();
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        { 
            UpdateResourceBars();
        }

        private void OnPlayerCreated(PlayerCreatedEvent e)
        {
            UpdateResourceBars();
        }


        private void UpdateResourceBars()
        {
            if (!Resources)
                return;
            ClearResourceBars();
            foreach ((Resource resource, float displayPoint) tuple in resources)
            {
                Resource resource = tuple.resource;
                float displayPoint = tuple.displayPoint;
                float value = Resources.GetResource(resource);
                float max = Resources.GetBaseMaxResource(resource);
                float percent = value / max;
                if (percent <= displayPoint)
                    BuildResourceBar(resource);
            }
        }
        
        private void ClearResourceBars()
        {
            for (int i = resourceBars.Count - 1; i >= 0; i--)
            {
                ResourceBar resourceBar = resourceBars[i];
                if (resourceBar)
                    Destroy(resourceBar.gameObject);
            }
            resourceBars.Clear();
        }

        private void BuildResourceBar(Resource resource)
        {
            GameObject prefab = Assets.Get<GameObject>("Resource Bar");
            GameObject gameObject = Instantiate(prefab, parent);
            ResourceBar resourceBar = gameObject.GetComponent<ResourceBar>();
            resourceBar.resource = resource;
            resourceBar.Initialize();
            resourceBar.ShowText(showAltText);
            resourceBars.Add(resourceBar);
        }

        private IEnumerator SwapTextRoutine()
        {
            while (true)
            {
                foreach (ResourceBar resourceBar in resourceBars)
                    resourceBar.ShowText(showAltText);
                showAltText = !showAltText;
                yield return wait;
            }
        }
    }
}
