using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

namespace DesignPatterns
{
    public class EventQueue : MonoBehaviour
    {
        public bool isRunning => currentEvent != null;
        public UnityEvent onQueueEmpty = new UnityEvent();
        public QueueableEvent currentEvent;
        public List<QueueableEvent> nextEvents = new List<QueueableEvent>();

        public void AddEvent(QueueableEvent newEvent)
        {
            nextEvents.Add(newEvent);
            newEvent.Setup(this);
            newEvent.onCanceled.AddListener(() => nextEvents.Remove(newEvent));
            currentEvent?.onEnded.RemoveListener(OnCurrentEventEnded);
        }

        public void ImmediatelyExecute(QueueableEvent newEvent)
        {
            foreach (QueueableEvent currentEvent in nextEvents)
            {
                currentEvent.End();
            }
            nextEvents.Clear();
            currentEvent.End();
            AddEvent(newEvent);
            ExecuteNextEvent();
        }

        [Button]
        public void ExecuteNextEvent()
        {
            if (isRunning)
            {
                Debug.Log("Already running!");
                return;
            }
            if (nextEvents.Count <= 0)
            {
                Debug.Log("There are no next events");
                return;
            }
            currentEvent = nextEvents[0];
            nextEvents.RemoveAt(0);
            ExecuteCurrentEvent();
        }

        private void ExecuteCurrentEvent()
        {
            currentEvent.Execute();
            currentEvent.onEnded.AddListener(OnCurrentEventEnded);
            currentEvent.onEnded.AddListener(ExecuteNextEvent);
        }

        private void OnCurrentEventEnded()
        {
            currentEvent = null;
            if (nextEvents.Count <= 0)
            {
                onQueueEmpty.Invoke();
                Debug.Log("Empty queue");
            }
        }
    }
}