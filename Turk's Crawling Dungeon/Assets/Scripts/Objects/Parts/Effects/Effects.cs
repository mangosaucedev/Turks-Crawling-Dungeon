using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;

namespace TCD.Objects.Parts.Effects
{
    public class Effects : Part
    {
        public List<Effect> activeEffects = new List<Effect>();

        public override string Name => "Effects";

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<BeforeTurnTickEvent>(this, OnBeforeTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<BeforeTurnTickEvent>(this);
        }

        public override bool HandleEvent<T>(T e)
        {
            bool successful = true;
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                Effect effect = activeEffects[i];
                if (!FireEvent(effect, e))
                    successful = false;
            }
            if (!successful)
                return false;
            return base.HandleEvent(e);
        }

        public void OnBeforeTurnTick(BeforeTurnTickEvent e)
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                Effect effect = activeEffects[i];
                effect.timeRemaining -= e.timeElapsed;
                if (effect.timeRemaining <= 0)
                    activeEffects.RemoveAt(i);
            }
        }

        public bool AddEffect(Effect effect, int time)
        {
            effect.effects = this;
            effect.time = time;
            effect.timeRemaining = time;
            if (!CanAddEffect(effect))
                return false;
            bool stacking = HasEffect(effect.Name);
            switch (effect.Stacking)
            {
                case EffectStacking.None:
                    if (!stacking)
                    {
                        activeEffects.Add(effect);
                        return AddedEffect(effect);
                    }
                    return false;
                case EffectStacking.RefreshCooldown:
                    if (!stacking)
                    {
                        activeEffects.Add(effect);
                        return AddedEffect(effect);
                    }
                    RefreshTime(effect.Name, time);
                    return false;
                case EffectStacking.StackDoNotRefreshCooldown:
                    activeEffects.Add(effect);
                    return AddedEffect(effect);
                case EffectStacking.StackRefreshCooldown:
                    activeEffects.Add(effect);
                    RefreshTime(effect.Name);
                    return AddedEffect(effect);
            }
            return false;
        }

        private bool CanAddEffect(Effect effect)
        {
            BeforeEffectAddedEvent e = LocalEvent.Get<BeforeEffectAddedEvent>();
            e.obj = parent;
            e.effect = effect;
            return FireEvent(parent, e);
        }

        public bool HasEffect(string name)
        {
            foreach (Effect effect in activeEffects)
            {
                if (effect.Name == name)
                    return true;
            }
            return false;
        }

        private void RefreshTime(string effectName, int refreshingEffectTime = 0)
        {
            int time = Mathf.Max(GetMaxTimeOfEffect(effectName), refreshingEffectTime);
            foreach (Effect effect in activeEffects)
            {
                if (effect.Name == effectName)
                    effect.timeRemaining = time;
            }
        }

        public int GetMaxTimeOfEffect(string effectName)
        {
            int time = 0;
            foreach (Effect effect in activeEffects)
            {
                if (effect.Name == effectName && effect.time > time)
                    time = effect.time;
            }
            return time;
        }

        private bool AddedEffect(Effect effect)
        {
            EffectAddedEvent e = LocalEvent.Get<EffectAddedEvent>();
            e.obj = parent;
            e.effect = effect;
            FireEvent(parent, e);
            if (parent.parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
                FloatingTextHandler.Draw(parent.transform.position, "+" + effect.Name, effect.Color);
            effect.OnApply();
            return true;
        }

        public bool RemoveEffect(string effectName)
        {
            bool successful = true;
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                Effect effect = activeEffects[i];
                if (effect.Name == effectName)
                {
                    if (!CanRemoveEffect(effect))
                    {
                        successful = false;
                        continue;
                    }
                    activeEffects.RemoveAt(i);
                    RemovedEffect(effect);
                }
            }
            return successful;
        }

        private bool CanRemoveEffect(Effect effect)
        {
            BeforeEffectRemovedEvent e = LocalEvent.Get<BeforeEffectRemovedEvent>();
            e.obj = parent;
            e.effect = effect;
            return FireEvent(parent, e);
        }

        private void RemovedEffect(Effect effect)
        {
            EffectRemovedEvent e = LocalEvent.Get<EffectRemovedEvent>();
            e.obj = parent;
            e.effect = effect;
            if (parent.parts.TryGet(out Visible visible) && visible.IsVisibleToPlayer())
                FloatingTextHandler.Draw(parent.transform.position, "-" + effect.Name, Color.red);
            effect.OnRemove();
            FireEvent(parent, e);
        }

        public int GetMaxTimeRemainingOfEffect(string effectName)
        {
            int time = 0;
            foreach (Effect effect in activeEffects)
            {
                if (effect.Name == effectName && effect.timeRemaining > time)
                    time = effect.timeRemaining;
            }
            return time;
        }

        public bool TryGetEffect(string name, out Effect effect)
        {
            effect = null;
            foreach (Effect e in activeEffects)
            {
                if (e.Name == name)
                {
                    effect = e;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetEffect<T>(out T effect) where T : Effect
        {
            effect = null;
            foreach (Effect e in activeEffects)
            {
                if (e is T)
                {
                    effect = (T) e;
                    return true;
                }
            }
            return false;
        }

        public int GetEffectStacks(string effectName)
        {
            int count = 0;
            foreach (Effect effect in activeEffects)
            {
                if (effect.Name == effectName)
                    count++;
            }
            return count;
        }
    }
}
