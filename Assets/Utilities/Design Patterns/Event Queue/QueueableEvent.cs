using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DesignPatterns
{
    public abstract class QueueableEvent : MonoBehaviour
    {
        public UnityEvent onEnded = new UnityEvent();
        public UnityEvent onCanceled = new UnityEvent();
        public EventQueue queue;
        public virtual void Execute() { }
        public virtual void Setup(EventQueue newQueue)
        {
            queue = newQueue;
        }
        public virtual void End()
        {
            onEnded.Invoke();
            Destroy(this);
        }
        public virtual void Cancel()
        {
            StopAllCoroutines();
            onCanceled.Invoke();
        }
    }
}