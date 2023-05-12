using UnityEngine;
using UnityEngine.Events;
using UltEvents;

namespace DesignPatterns
{
    public abstract class PoolableGO : MonoBehaviour, IPoolable
    {
        public virtual void OnPoolGet() { }
        public virtual void OnPoolRelease() { }
        public virtual void OnPoolDestroy() { }
        public UltEvent onRelease { get; set; }
        public virtual void Release()
        {
            onRelease.Invoke();
        }
    }
}