using UnityEngine;
using UnityEngine.Events;
using UltEvents;
using Sirenix.OdinInspector;

namespace DesignPatterns
{
    public abstract class Poolable : SerializedMonoBehaviour
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