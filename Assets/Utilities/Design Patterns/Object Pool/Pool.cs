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
    public class Pool<T> where T : Poolable<T>
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
                        newObject = (T)Poolable<T>.GetNewInstance(newObjectName, parent);
                    }
                    newObject.Init(this);
                    inactiveObjects.Add(newObject);
                    allObjects.Add(newObject);
                    return newObject;
                },
                obj =>
                {
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