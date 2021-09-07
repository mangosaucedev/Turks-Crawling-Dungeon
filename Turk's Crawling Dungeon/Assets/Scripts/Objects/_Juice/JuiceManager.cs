using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public class JuiceManager : MonoBehaviour
    {
        private List<JuiceAnimation> inProgress = new List<JuiceAnimation>();
        private Queue<JuiceAnimation> queue = new Queue<JuiceAnimation>();

        private void OnEnable()
        {
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        private void OnDisable()
        {
            EventManager.StopListening<AfterTurnTickEvent>(this);
            StopAllCoroutines();
        }

        private void Update()
        {
            
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            inProgress.Clear();
            PlayQueuedAnimations();
        }

        public void Enqueue(JuiceAnimation juiceAnimation)
        {
            queue.Enqueue(juiceAnimation);
        }

        private void PlayQueuedAnimations()
        {
            while (queue.Count > 0)
            {
                JuiceAnimation juice = queue.Dequeue();
                StartPlaying(juice);
            }
        }

        private void StartPlaying(JuiceAnimation juice)
        {
            inProgress.Add(juice);
            juice.Start();
        }
    }
}
