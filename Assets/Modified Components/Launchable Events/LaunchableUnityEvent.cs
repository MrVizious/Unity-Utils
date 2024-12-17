using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityEngine.Events
{
    public class LaunchableUnityEvent<T>
    {
        public UnityEvent<T> @event = new UnityEvent<T>();
        [Button]
        public void InvokeEvent(T parameter)
        {
            @event.Invoke(parameter);
        }
    }
    public class LaunchableUnityEvent
    {
        public UnityEvent @event = new UnityEvent();
        [Button]
        public void InvokeEvent()
        {
            @event.Invoke();
        }
    }

}