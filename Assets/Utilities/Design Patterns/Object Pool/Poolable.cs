using UnityEngine;
using UnityEngine.Events;
using UltEvents;

namespace DesignPatterns
{
    public abstract class Poolable : MonoBehaviour
    {
        public virtual void OnPoolGet() { }
        public virtual void OnPoolRelease() { }
        public virtual void OnPoolDestroy() { }
        public delegate void ReleaseEvent();
        public UltEvent onRelease;
        public virtual void Release()
        {
            onRelease.Invoke();
        }
    }
}