using System;
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
        [Obsolete("Use Invoke() instead")]
        public void InvokeEvent(T parameter)
        {
            Invoke(parameter);
        }
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
        [Obsolete("Use Invoke() instead")]
        public void InvokeEvent()
        {
            Invoke();
        }
        [Button]
        public void Invoke()
        {
            @event.Invoke();
        }
    }

}