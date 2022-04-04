using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public abstract class CinematicView : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            EventManager.Listen<CinematicEndedEvent>(this, OnCinematicEnded);
        }

        protected virtual void OnDisable()
        {
            EventManager.StopListening<CinematicEndedEvent>(this);
        }

        protected virtual void OnCinematicEnded(CinematicEndedEvent e)
        {
            ViewManager.Close(name);
        }
    }
}
