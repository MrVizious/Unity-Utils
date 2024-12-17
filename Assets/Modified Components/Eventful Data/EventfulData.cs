using System;
using UnityEngine;
using UltEvents;
using Sirenix.OdinInspector;

namespace EventfulData
{
    /// <summary>
    /// Base class for Eventful data that triggers an event when the value changes.
    /// </summary>
    [Serializable]
    public abstract class EventfulData<T>
    {
        public bool debug = false;
        [SerializeField]
        [OnValueChanged("OnValueChanged")]
        protected T _value;

        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public UltEvent<T> onValueChanged = new UltEvent<T>();

        /// <summary>
        /// The value property with change notification.
        /// </summary>
        public T value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged();
                }
            }
        }

        protected virtual T OnValueChanged()
        {
            if (debug)
            {
                Debug.Log($"New value is {value}");
            }
            onValueChanged?.Invoke(value);
            return value;
        }

        protected EventfulData(T initialValue)
        {
            _value = initialValue;
        }

        public override string ToString() => _value?.ToString() ?? "null";
    }

    /// <summary>
    /// Eventful data for reference types.
    /// </summary>
    [Serializable]
    public class EventfulClass<T> : EventfulData<T> where T : class
    {
        public EventfulClass(T initialValue = null) : base(initialValue) { }

        public static implicit operator T(EventfulClass<T> data) => data.value;
        public static implicit operator EventfulClass<T>(T value) => new EventfulClass<T>(value);
    }

    /// <summary>
    /// Eventful data for value types.
    /// </summary>
    [Serializable]
    public class EventfulStruct<T> : EventfulData<T> where T : struct
    {
        public EventfulStruct(T initialValue = default) : base(initialValue) { }

        public static implicit operator T(EventfulStruct<T> data) => data.value;
        public static implicit operator EventfulStruct<T>(T value) => new EventfulStruct<T>(value);
    }
}
