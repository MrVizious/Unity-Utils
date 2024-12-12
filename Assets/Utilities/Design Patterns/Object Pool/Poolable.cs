using UnityEngine;
using UnityEngine.Events;
using UltEvents;
using Sirenix.OdinInspector;

namespace DesignPatterns
{
    public class Poolable : SerializedMonoBehaviour
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
            Destroy(gameObject);
        }
        public UltEvent onRelease;
        [Button]
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