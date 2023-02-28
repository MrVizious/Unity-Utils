using System;
using UnityEngine;
using UnityEngine.Pool;

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
        private ObjectPool<T> _pool;

        private ObjectPool<T> pool
        {
            get
            {
                if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool;
            }
            set => _pool = value;
        }

        public Pool(int initial = 10, int max = 100, bool collectionChecks = true, T prefab = null)
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
                        newObject = (T)Poolable<T>.GetNewInstance();
                    }
                    newObject.Init(this);
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
                collectionChecks,
                initial,
                max);
        }

        #region Getters
        public T Get() => pool.Get();
        public void Release(T obj) => pool.Release(obj);
        #endregion
    }
}