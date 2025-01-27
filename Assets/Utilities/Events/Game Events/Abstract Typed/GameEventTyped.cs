using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    public abstract class GameEventTyped<T> : GameEvent
    {
        public T value;
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        protected new readonly List<GameEventListenerTyped<T>> eventListeners =
            new List<GameEventListenerTyped<T>>();

        public override void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(value);
        }
        public void Raise(T customValue)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(customValue);
        }

        public void RegisterListener(GameEventListenerTyped<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerTyped<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}