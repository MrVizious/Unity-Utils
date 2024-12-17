using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UltEvents
{
    public class LaunchableUltEvent<T>
    {
        public UltEvent<T> @event = new UltEvent<T>();
        [Button]
        public void InvokeEvent(T parameter)
        {
            @event.Invoke(parameter);
        }
    }
    public class LaunchableUltEvent
    {
        public UltEvent @event = new UltEvent();
        [Button]
        public void InvokeEvent()
        {
            @event.Invoke();
        }
    }

}