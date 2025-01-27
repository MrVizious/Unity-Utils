using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UltEvents
{
    public class LaunchableUltEvent<T>
    {
        public UltEvent<T> @event = new UltEvent<T>();
        [Button(Stretch = true)]
        public void Invoke(T parameter)
        {
            @event.Invoke(parameter);
        }
    }
    public class LaunchableUltEvent
    {
        public UltEvent @event = new UltEvent();
        [Button]
        public void Invoke()
        {
            @event.Invoke();
        }
    }

}