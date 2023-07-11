using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
namespace DesignPatterns
{
    public abstract class QueueableEvent : MonoBehaviour
    {
        public UnityEvent onEnded = new UnityEvent();
        public UnityEvent onCanceled = new UnityEvent();
        public EventQueue queue;
        public virtual async UniTask Execute() { }
        public virtual void Setup(EventQueue newQueue)
        {
            queue = newQueue;
        }
        public virtual void End()
        {
            Debug.Log("Ending event " + this);
            onEnded.Invoke();
            Destroy(this);
        }
        public virtual void Cancel()
        {
            StopAllCoroutines();
            onCanceled.Invoke();
            Destroy(this);
        }
    }
}