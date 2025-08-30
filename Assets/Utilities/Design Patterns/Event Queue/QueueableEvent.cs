using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
namespace DesignPatterns
{
    public abstract class QueueableEvent : MonoBehaviour
    {
        public UnityEvent onEnded = new UnityEvent();
        public UnityEvent onCanceled = new UnityEvent();
        public EventQueue queue;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async UniTask Execute() { }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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