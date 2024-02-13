using UnityEngine;
using UnityEngine.Events;
using UltEvents;

namespace DesignPatterns
{
    public abstract class Poolable : MonoBehaviour
    {
        public virtual void OnPoolGet()
        {
            gameObject.SetActive(true);
        }
        public virtual void OnPoolRelease()
        {
            gameObject.SetActive(false);
        }
        public virtual void OnPoolDestroy()
        {
            gameObject.SetActive(false);
        }
        public UltEvent onRelease;
        public virtual void Release()
        {
            onRelease.Invoke();
        }

        protected virtual void OnDisable()
        {
            Release();
        }
    }
}