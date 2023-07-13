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
            Destroy(this);
            onEnded.Invoke();
        }
        public virtual void Cancel()
        {
            StopAllCoroutines();
            Debug.Log("Canceled event");
            Destroy(this);
            onCanceled.Invoke();
        }
    }
}