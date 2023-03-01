using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public abstract class SingletonReferenceSO<T> : ScriptableObject where T : Singleton<T>
    {
        public abstract T Instance { get; }
    }
}
