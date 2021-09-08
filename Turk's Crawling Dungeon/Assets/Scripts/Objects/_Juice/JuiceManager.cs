using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class JuiceManager : MonoBehaviour
    {
        private List<JuiceAnimation> inProgress = new List<JuiceAnimation>();

        private void OnEnable()
        {
            EventManager.Listen<BeforeTurnTickEvent>(this, OnBeforeTurnTick);
        }

        private void OnDisable()
        {
            EventManager.StopListening<BeforeTurnTickEvent>(this);
            StopAllCoroutines();
        }

        private void Update()
        {
            for (int i = inProgress.Count - 1; i >= 0; i--)
            { 
                JuiceAnimation animation = inProgress[i];
                UpdateAnimation(animation);
            }
        }

        private void UpdateAnimation(JuiceAnimation animation)
        {           
            if (animation.CanPerform())
            {
                if (!animation.hasStarted)
                {
                    animation.hasStarted = true;
                    animation.Start();
                }
                animation.Update();
                if (animation.IsFinished())
                    RemoveAnimation(animation);
            }
        }

        private void RemoveAnimation(JuiceAnimation animation)
        {
            animation.End();
            inProgress.Remove(animation);
        }

        private void OnBeforeTurnTick(BeforeTurnTickEvent e)
        {
            RemoveAllAnimations();
        }

        private void RemoveAllAnimations()
        {
            for (int i = inProgress.Count - 1; i >= 0; i--)
            {
                JuiceAnimation animation = inProgress[i];
                RemoveAnimation(animation);
            }
            inProgress.Clear();
        }

        public void AddAnimation(JuiceAnimation juiceAnimation)
        {
            inProgress.Add(juiceAnimation);
        }
    }
}
