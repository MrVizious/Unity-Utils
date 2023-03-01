using UnityEngine;

namespace DesignPatterns
{
    public abstract class Poolable<T> : MonoBehaviour where T : Poolable<T>
    {
        protected Pool<T> pool;
        public virtual void Init(Pool<T> newPool)
        {
            pool = newPool;
        }
        public static T GetNewInstance(string newName = "New Pooled Object", Transform newParent = null)
        {
            GameObject go = new GameObject(newName);
            go.transform.SetParent(newParent);
            return go.AddComponent<T>();
        }
        public virtual void OnPoolGet() { }
        public virtual void OnPoolRelease() { }
        public virtual void OnPoolDestroy() { }
        public virtual void Release()
        {
            pool.Release((T)this);
        }
    }
}
