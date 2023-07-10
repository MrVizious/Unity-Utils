using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public class EventQueue : MonoBehaviour
    {
        public QueueableEvent currentEvent;
        public List<QueueableEvent> nextEvents = new List<QueueableEvent>();

        public void AddEvent(QueueableEvent newEvent)
        {
            nextEvents.Add(newEvent);
            newEvent.Setup(this);
            newEvent.onCanceled.AddListener(() => nextEvents.Remove(newEvent));
        }

        public void ImmediatelyExecute(QueueableEvent newEvent)
        {
            foreach (QueueableEvent currentEvent in nextEvents)
            {
                currentEvent.End();
            }
            nextEvents.Clear();
            currentEvent.End();
        }

        public void ExecuteNextEvent()
        {
            if (nextEvents.Count < 1) return;
            currentEvent = nextEvents[0];
            nextEvents.RemoveAt(0);
            ExecuteCurrentEvent();
        }

        private void ExecuteCurrentEvent()
        {
            currentEvent.Execute();
            currentEvent.onEnded.AddListener(ExecuteNextEvent);
        }
    }
}