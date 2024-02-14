using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using RuntimeSet;

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
        private T _prefab;
        public HashSet<T> activeObjects = new HashSet<T>();
        public HashSet<T> inactiveObjects = new HashSet<T>();
        public HashSet<T> allObjects = new HashSet<T>();

        public ObjectPool<T> pool
        {
            get; protected set;
        }

        public Pool(int defaultCapacity = 10, int maxSize = 100, bool collectionCheck = true, string newObjectName = "Pooled Object", T prefab = null, Transform parent = null)
        {
            _prefab = prefab;
            pool = new ObjectPool<T>(
                () =>
                {
                    T newObject;
                    if (_prefab != null)
                    {
                        newObject = GameObject.Instantiate(_prefab);
                    }
                    else
                    {
                        newObject = GetNewInstance(newObjectName, parent);
                    }
                    // Subscribe and unsubscribe
                    newObject.onRelease += () => Release(newObject);
                    inactiveObjects.Add(newObject);
                    allObjects.Add(newObject);
                    return newObject;
                },
                obj =>
                {
                    Debug.Log("Getting new item from pool");
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


        public T GetNewInstance(string newName = "New Pooled Object", Transform newParent = null)
        {
            GameObject go = new GameObject(newName);
            go.transform.SetParent(newParent);
            return go.AddComponent<T>();
        }

        #region Getters
        public T Get()
        {
            T obj = pool.Get();
            inactiveObjects.Remove(obj);
            activeObjects.Add(obj);
            return obj;
        }
        public void Release(T obj)
        {
            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
            pool.Release(obj);
        }
        #endregion
    }
}