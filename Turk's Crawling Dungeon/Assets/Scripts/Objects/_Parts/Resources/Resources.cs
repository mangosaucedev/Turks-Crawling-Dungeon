using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Resources : Part 
    {
        [SerializeField] private float hitpoints;
        [SerializeField] private float hitpointRegen;
        [SerializeField] private float hitpointRegenPoint;
        [SerializeField] private float stamina;
        [SerializeField] private float staminaRegen;
        [SerializeField] private float staminaRegenPoint;
        [SerializeField] private float psi;
        [SerializeField] private float psiRegen;
        [SerializeField] private float psiRegenPoint;
        [SerializeField] private float oxygen;
        [SerializeField] private float oxygenRegen;
        [SerializeField] private float oxygenRegenPoint;
        [SerializeField] private float hunger;
        [SerializeField] private float hungerRegen;
        [SerializeField] private float hungerRegenPoint;
        [SerializeField] private float thirst;
        [SerializeField] private float thirstRegen;
        [SerializeField] private float thirstRegenPoint;
        [SerializeField] private float morale;
        [SerializeField] private float moraleRegen;
        [SerializeField] private float moraleRegenPoint;
        [SerializeField] private float stimulation;
        [SerializeField] private float stimulationRegen;
        [SerializeField] private float stimulationRegenPoint;
        [SerializeField] private bool regenerates;
        [SerializeField] private Dictionary<Resource, float> resources = new Dictionary<Resource, float>();
        [SerializeField] private Dictionary<Resource, float> maxResources = new Dictionary<Resource, float>();
        [SerializeField] private Dictionary<Resource, float> regen = new Dictionary<Resource, float>();
        [SerializeField] private Dictionary<Resource, float> regenPoint = new Dictionary<Resource, float>();

        public float Hitpoints
        {
            get => hitpoints;
            set => hitpoints = value;
        }

        public float HitpointRegen
        {
            get => hitpointRegen;
            set => hitpointRegen = value;
        }

        public float HitpointRegenPoint
        {
            get => hitpointRegenPoint;
            set => hitpointRegenPoint = value;
        }

        public float Stamina
        {
            get => stamina;
            set => stamina = value;
        }

        public float StaminaRegen
        {
            get => staminaRegen;
            set => staminaRegen = value;
        }

        public float StaminaRegenPoint
        {
            get => staminaRegenPoint;
            set => staminaRegenPoint = value;
        }

        public float Psi
        {
            get => psi;
            set => psi = value;
        }

        public float PsiRegen
        {
            get => psiRegen;
            set => psiRegen = value;
        }

        public float PsiRegenPoint
        {
            get => psiRegenPoint;
            set => psiRegenPoint = value;
        }

        public float Oxygen
        {
            get => oxygen;
            set => oxygen = value;
        }

        public float OxygenRegen
        {
            get => oxygenRegen;
            set => oxygenRegen = value;
        }

        public float OxygenRegenPoint
        {
            get => oxygenRegenPoint;
            set => oxygenRegenPoint = value;
        }

        public float Hunger
        {
            get => hunger;
            set => hunger = value;
        }

        public float HungerRegen
        {
            get => hungerRegen;
            set => hungerRegen = value;
        }

        public float HungerRegenPoint
        {
            get => hungerRegenPoint;
            set => hungerRegenPoint = value;
        }

        public float Thirst
        {
            get => thirst;
            set => thirst = value;
        }
       
        public float ThirstRegen
        {
            get => thirstRegen;
            set => thirstRegen = value;
        }
       
        public float ThirstRegenPoint
        {
            get => thirstRegenPoint;
            set => thirstRegenPoint = value;
        }

        public float Morale
        {
            get => morale;
            set => morale = value;
        }

        public float MoraleRegen
        {
            get => moraleRegen;
            set => moraleRegen = value;
        }

        public float MoraleRegenPoint
        {
            get => moraleRegenPoint;
            set => moraleRegenPoint = value;
        }

        public float Stimulation
        {
            get => stimulation;
            set => stimulation = value;
        }

        public float StimulationRegen
        {
            get => stimulationRegen;
            set => stimulationRegen = value;
        }

        public float StimulationRegenPoint
        {
            get => stimulationRegenPoint;
            set => stimulationRegenPoint = value;
        }

        public bool Regenerates
        {
            get => regenerates;
            set => regenerates = value;
        }

        public override string Name => "Resources";

        protected override void Start()
        {
            base.Start();
            resources[Resource.Hitpoints] = Hitpoints; 
            maxResources[Resource.Hitpoints] = Hitpoints; 
            regen[Resource.Hitpoints] = HitpointRegen; 
            regenPoint[Resource.Hitpoints] = HitpointRegenPoint; 
            resources[Resource.Stamina] = Stamina;
            maxResources[Resource.Stamina] = Stamina;
            regen[Resource.Stamina] = StaminaRegen;
            regenPoint[Resource.Stamina] = StaminaRegenPoint;
            resources[Resource.Psi] = Psi;
            maxResources[Resource.Psi] = Psi;
            regen[Resource.Psi] = PsiRegen;
            regenPoint[Resource.Psi] = PsiRegenPoint;
            resources[Resource.Oxygen] = Oxygen;
            maxResources[Resource.Oxygen] = Oxygen;
            regen[Resource.Oxygen] = OxygenRegen;
            regenPoint[Resource.Oxygen] = OxygenRegenPoint;
            resources[Resource.Hunger] = Hunger;
            maxResources[Resource.Hunger] = Hunger;
            regen[Resource.Hunger] = HungerRegen;
            regenPoint[Resource.Hunger] = HungerRegenPoint;
            resources[Resource.Thirst] = Thirst;
            maxResources[Resource.Thirst] = Thirst;
            regen[Resource.Thirst] = ThirstRegen;
            regenPoint[Resource.Thirst] = ThirstRegenPoint;
            resources[Resource.Morale] = Morale;
            maxResources[Resource.Morale] = Morale;
            regen[Resource.Morale] = MoraleRegen;
            regenPoint[Resource.Morale] = MoraleRegenPoint;
            resources[Resource.Stimulation] = Stimulation;
            maxResources[Resource.Stimulation] = Stimulation;
            regen[Resource.Stimulation] = StimulationRegen;
            regenPoint[Resource.Stimulation] = StimulationRegenPoint;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTickEvent);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnAfterTurnTickEvent(AfterTurnTickEvent e)
        {
            if (!Regenerates)
                return;
            float regenMultiplier = e.timeElapsed / TimeInfo.TIME_PER_STANDARD_TURN;
            Regen(Resource.Hitpoints, regenMultiplier);
            Regen(Resource.Stamina, regenMultiplier);
            Regen(Resource.Psi, regenMultiplier);
            Regen(Resource.Oxygen, regenMultiplier);
            Regen(Resource.Hunger, regenMultiplier);
            Regen(Resource.Thirst, regenMultiplier);
            Regen(Resource.Morale, regenMultiplier);
            Regen(Resource.Stimulation, regenMultiplier);
        }

        private void Regen(Resource resource, float multiplier)
        {
            float currentResource = GetResource(resource);
            float maxResource = GetMaxResource(resource);
            float resourcePoint = currentResource / maxResource;            
            float regenPoint = GetResourceRegenPoint(resource);
            if (resourcePoint < regenPoint)
            {
                float regen = GetResourceRegen(resource) * multiplier;
                ModifyResource(resource, regen);
            }
        }

        public float GetResource(Resource resource)
        {
            GetResourceEvent e = LocalEvent.Get<GetResourceEvent>();
            e.obj = parent;
            e.resource = resource;
            FireEvent(parent, e);
            return e.amount;
        }

        public float GetMaxResource(Resource resource)
        {
            GetMaxResourceEvent e = LocalEvent.Get<GetMaxResourceEvent>();
            e.obj = parent;
            e.resource = resource;
            FireEvent(parent, e);
            return e.amount;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == GetResourceEvent.id)
                OnGetResource(e);
            if (e.Id == GetMaxResourceEvent.id)
                OnGetMaxResource(e);
            if (e.Id == GetResourceRegenEvent.id)
                OnGetResourceRegen(e);
            if (e.Id == GetResourceRegenPointEvent.id)
                OnGetResourceRegenPoint(e);
            if (e.Id == ResourceModifiedEvent.id)
                OnResourceModified(e);
            return base.HandleEvent(e);
        }

        private void OnGetResource(LocalEvent e)
        {
            GetResourceEvent getResourceEvent = (GetResourceEvent) e;
            if (getResourceEvent.obj != parent)
                return;
            getResourceEvent.amount = GetBaseResource(getResourceEvent.resource);
            getResourceEvent.amount = Mathf.Clamp(getResourceEvent.amount, 0, GetMaxResource(getResourceEvent.resource));
        }

        public float GetBaseResource(Resource resource)
        { 
            if (!resources.TryGetValue(resource, out float value))
            {
                value = 0;
                resources[resource] = value;
            }
            return value;
        }

        private void OnGetMaxResource(LocalEvent e)
        {
            GetMaxResourceEvent getMaxResourceEvent = (GetMaxResourceEvent) e;
            if (getMaxResourceEvent.obj != parent)
                return;
            getMaxResourceEvent.amount += GetBaseMaxResource(getMaxResourceEvent.resource);
        }

        public float GetBaseMaxResource(Resource resource)
        {
            if (!maxResources.TryGetValue(resource, out float value))
            {
                value = 0;
                maxResources[resource] = value;
            }
            return value;
        }

        private void OnGetResourceRegen(LocalEvent e)
        {
            GetResourceRegenEvent getResourceRegenEvent = (GetResourceRegenEvent) e;
            getResourceRegenEvent.amount += GetBaseResourceRegen(getResourceRegenEvent.resource);
        }

        public float GetBaseResourceRegen(Resource resource)
        {
            if (!regen.TryGetValue(resource, out float value))
            {
                value = 0;
                regen[resource] = value;
            }
            return value;
        }

        private void OnGetResourceRegenPoint(LocalEvent e)
        {
            GetResourceRegenPointEvent getResourceRegenPointEvent = (GetResourceRegenPointEvent) e;
            getResourceRegenPointEvent.amount += GetBaseResourceRegenPoint(getResourceRegenPointEvent.resource);
        }

        public float GetBaseResourceRegenPoint(Resource resource)
        {
            if (!regenPoint.TryGetValue(resource, out float value))
            {
                value = 0;
                regenPoint[resource] = value;
            }
            return value;
        }

        private void OnResourceModified(LocalEvent e)
        {
            ResourceModifiedEvent resourceModifiedEvent = (ResourceModifiedEvent) e;
            Resource resource = resourceModifiedEvent.resource;
            switch (resource)
            {
                case Resource.Hitpoints:
                case Resource.Oxygen:
                case Resource.Hunger:
                case Resource.Thirst:
                    if (GetResource(resource) <= 0)
                        parent.Destroy();
                    break;
                default:
                    break;
            }
        }

        public bool ModifyResource(Resource resource, float amount)
        {
            if (!CanModifyResource(resource, amount))
                return false;
            float value = GetResource(resource);
            resources[resource] = Mathf.Clamp(value + amount, 0, GetMaxResource(resource));
            OnModifyResource(resource, amount);
            return true;
        }

        private bool CanModifyResource(Resource resource, float amount)
        {
            BeforeResourceModifiedEvent e = LocalEvent.Get<BeforeResourceModifiedEvent>();
            e.obj = parent;
            e.resource = resource;
            e.amount = amount;
            return FireEvent(parent, e);
        }

        private void OnModifyResource(Resource resource, float amount)
        {
            ResourceModifiedEvent e = LocalEvent.Get<ResourceModifiedEvent>();
            e.obj = parent;
            e.resource = resource;
            e.amount = amount;
            FireEvent(parent, e);
        }

        public float GetResourceRegen(Resource resource)
        {
            GetResourceRegenEvent e = LocalEvent.Get<GetResourceRegenEvent>();
            e.obj = parent;
            e.resource = resource;
            FireEvent(parent, e);
            return e.amount;
        }

        public float GetResourceRegenPoint(Resource resource)
        {
            GetResourceRegenPointEvent e = LocalEvent.Get<GetResourceRegenPointEvent>();
            e.obj = parent;
            e.resource = resource;
            FireEvent(parent, e);
            return e.amount;
        }
    }
}
