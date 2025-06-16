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
    public class EventfulData<T>
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

        protected T OnValueChanged()
        {
            if (debug)
            {
                Debug.Log($"New value is {value}");
            }
            onValueChanged?.Invoke(value);
            return value;
        }

        public EventfulData(T initialValue = default, bool debug = false)
        {
            _value = initialValue;
            this.debug = debug; ;
        }

        public override string ToString() => _value?.ToString() ?? "null";
        // public static implicit operator T(EventfulData<T> data) => data.value;
        // public static implicit operator EventfulData<T>(T value) => new EventfulData<T>(value);
    }

    /// <summary>
    /// Eventful data for reference types.
    /// </summary>
    [Serializable]
    [Obsolete("Use EventfulData instead")]
    public class EventfulClass<T> : EventfulData<T>
    {
        public EventfulClass(T initialValue = default, bool debug = false) : base(initialValue, debug) { }

        public static implicit operator T(EventfulClass<T> data) => data.value;
        public static implicit operator EventfulClass<T>(T value) => new EventfulClass<T>(value);
    }

    /// <summary>
    /// Eventful data for value types.
    /// </summary>
    [Serializable]
    [Obsolete("Use EventfulData instead")]
    public class EventfulStruct<T> : EventfulData<T> where T : struct
    {
        public EventfulStruct(T initialValue = default, bool debug = false) : base(initialValue, debug) { }

        public static implicit operator T(EventfulStruct<T> data) => data.value;
        public static implicit operator EventfulStruct<T>(T value) => new EventfulStruct<T>(value);
    }
}
