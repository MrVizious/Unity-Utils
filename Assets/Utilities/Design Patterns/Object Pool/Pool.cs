using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using RuntimeSet;
using Sirenix.OdinInspector;

namespace DesignPatterns
{

    /// <summary>
    /// A simple base class to simplify object pooling in Unity 2021.
    /// Source: https://github.com/Matthew-J-Spencer/Object-Pooler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class Pool<T> where T : Poolable
    {
        [SerializeField]
        private T _prefab;
        private int _maxSize;
        public List<T> activeObjects = new List<T>();
        public List<T> inactiveObjects = new List<T>();
        public List<T> allObjects = new List<T>();

        public ObjectPool<T> pool
        {
            get; protected set;
        }

        public enum GetBehaviourWhenFull
        {
            DeleteOldest,
            NotCreateNew
        }
        public GetBehaviourWhenFull getBehaviourWhenFull = GetBehaviourWhenFull.DeleteOldest;

        public Pool(int defaultCapacity = 10, int maxSize = 100, bool collectionCheck = true, string newObjectName = "Pooled Object", T prefab = null, Transform parent = null, GetBehaviourWhenFull newGetBehaviourWhenFull = GetBehaviourWhenFull.DeleteOldest)
        {
            _prefab = prefab;
            _maxSize = maxSize;
            pool = new ObjectPool<T>(
                () =>
                {
                    T newObject;
                    if (_prefab != null)
                    {
                        newObject = GameObject.Instantiate(_prefab, parent);
                        newObject.name = newObjectName;
                    }
                    else
                    {
                        newObject = GetNewInstance(newObjectName, parent);
                    }
                    // Subscribe and unsubscribe
                    newObject.onRelease = null;
                    newObject.onRelease += () => Release(newObject);
                    inactiveObjects.Add(newObject);
                    allObjects.Add(newObject);
                    return newObject;
                },
                obj =>
                {
                    //Debug.Log("Getting new item from pool");
                    obj.OnPoolGet();
                },
                obj =>
                {
                    obj.OnPoolRelease();
                }
                ,
                obj =>
                {
                    obj.OnPoolDestroy();
                },
                collectionCheck,
                defaultCapacity,
                maxSize);
        }



        #region Getters
        [Button]
        public T Get()
        {
            if (pool.CountActive >= _maxSize)
            {
                if (getBehaviourWhenFull == GetBehaviourWhenFull.DeleteOldest)
                {
                    activeObjects[0].Release();
                    return Get();
                }
                if (getBehaviourWhenFull == GetBehaviourWhenFull.NotCreateNew)
                {
                    return null;
                }
            }
            T obj = pool.Get();
            inactiveObjects.Remove(obj);
            activeObjects.Add(obj);
            return obj;
        }
        /// <summary>
        /// Used internally to create a new instance if no prefab was provided
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="newParent"></param>
        /// <returns>The new instance created from no prefab</returns>
        [Button]
        private T GetNewInstance(string newName = "New Pooled Object", Transform newParent = null)
        {
            GameObject go = new GameObject(newName);
            go.transform.SetParent(newParent);
            return go.AddComponent<T>();
        }
        #endregion
        #region Releases
        [Button]
        public void Release(T obj)
        {
            if (!activeObjects.Contains(obj))
            {
                return;
            }
            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
            pool.Release(obj);
        }
        [Button]
        public void ReleaseAllActive()
        {
            foreach (T activeObj in new HashSet<T>(activeObjects))
            {
                Release(activeObj);
            }
        }
        #endregion
    }
}