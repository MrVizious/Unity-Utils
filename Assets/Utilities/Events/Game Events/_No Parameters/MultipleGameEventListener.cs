using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
namespace GameEvents
{
    public class MultipleGameEventListener : GameEventListener
    {
        [Tooltip("Event to register with.")]
        public List<GameEvent> Events;

        private void OnEnable()
        {
            foreach (GameEvent gameEvent in Events)
            {
                gameEvent.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            foreach (GameEvent gameEvent in Events)
            {
                gameEvent.UnregisterListener(this);
            }
        }
    }
}