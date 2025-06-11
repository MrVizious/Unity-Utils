using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UltEvents
{
    public class LaunchableUltEvent<T>
    {
        public UltEvent<T> @event = new UltEvent<T>();
        [Obsolete("Use Invoke() instead")]
        public void InvokeEvent(T parameter)
        {
            @event.Invoke(parameter);
        }
        [Button]
        public void Invoke(T parameter)
        {
            @event.Invoke(parameter);
        }
        // Implicit conversion to UltEvent<T> to allow += subscriptions
        public static implicit operator UltEvent<T>(LaunchableUltEvent<T> launchableEvent)
            => launchableEvent.@event;

        // Expose += and -= so that subscriptions work seamlessly
        public LaunchableUltEvent<T> AddListener(System.Action<T> listener)
        {
            @event += listener;
            return this;
        }

        public LaunchableUltEvent<T> RemoveListener(System.Action<T> listener)
        {
            @event -= listener;
            return this;
        }


        // Operator overloads for += and -=
        public static LaunchableUltEvent<T> operator +(LaunchableUltEvent<T> launchableEvent, System.Action<T> listener)
        {
            launchableEvent.@event += listener;
            return launchableEvent;
        }

        public static LaunchableUltEvent<T> operator -(LaunchableUltEvent<T> launchableEvent, System.Action<T> listener)
        {
            launchableEvent.@event -= listener;
            return launchableEvent;
        }
    }
    public class LaunchableUltEvent
    {
        public UltEvent @event = new UltEvent();
        [Obsolete("Use Invoke() instead")]
        public void InvokeEvent()
        {
            @event.Invoke();
        }
        [Button]
        public void Invoke()
        {
            @event.Invoke();
        }
        // Implicit conversion to UltEvent to allow += subscriptions
        public static implicit operator UltEvent(LaunchableUltEvent launchableEvent)
            => launchableEvent.@event;

        // Expose += and -= so that subscriptions work seamlessly
        public LaunchableUltEvent AddListener(System.Action listener)
        {
            @event += listener;
            return this;
        }

        public LaunchableUltEvent RemoveListener(System.Action listener)
        {
            @event -= listener;
            return this;
        }

        public static LaunchableUltEvent operator +(LaunchableUltEvent launchableEvent, System.Action listener)
        {
            launchableEvent.@event += listener;
            return launchableEvent;
        }

        public static LaunchableUltEvent operator -(LaunchableUltEvent launchableEvent, System.Action listener)
        {
            launchableEvent.@event -= listener;
            return launchableEvent;
        }
    }

}