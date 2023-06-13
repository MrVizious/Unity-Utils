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
        public virtual void Setup() { }
        public virtual void Execute() { }
        public virtual void End()
        {
            onEnded.Invoke();
        }
        public virtual void Cancel()
        {
            onCanceled.Invoke();
        }
    }
}