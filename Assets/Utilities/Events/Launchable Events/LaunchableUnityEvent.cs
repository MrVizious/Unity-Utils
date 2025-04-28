using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityEngine.Events
{
    [System.Serializable]
    public class LaunchableUnityEvent<T>
    {
        public UnityEvent<T> @event = new UnityEvent<T>();
        [Button]
        public void Invoke(T parameter)
        {
            @event.Invoke(parameter);
        }
    }
    [System.Serializable]
    public class LaunchableUnityEvent
    {
        public UnityEvent @event = new UnityEvent();
        [Button]
        public void Invoke()
        {
            @event.Invoke();
        }
    }

}