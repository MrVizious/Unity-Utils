using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace GameEvents
{
    public abstract class GameEventListenerTyped<T> : MonoBehaviour

    {
        [Tooltip("Event to register with.")]
        public GameEventTyped<T> Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UltEvent<T> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            Response.Invoke(value);
        }
    }
}