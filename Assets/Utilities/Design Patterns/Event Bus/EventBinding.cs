using System;

namespace DesignPatterns.EventBus
{
    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        Action<T> onEvent = _ => { };
        Action onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => onEvent;
            set => onEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs
        {
            get => onEventNoArgs;
            set => onEventNoArgs = value;
        }

        /// <summary>
        /// Construct an EventBinding with an action that takes arguments
        /// </summary>
        /// <param name="onEvent">Event with arguments</param>
        public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;

        /// <summary>
        /// Construct an EventBinding with an action that takes no arguments
        /// </summary>
        /// <param name="onEvent">Event with no arguments</param>
        public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

        /// <summary>
        /// Adds an action with no arguments to the EventBinding 
        /// </summary>
        /// <param name="action">Action with no arguments</param>
        public void Add(Action action) => onEventNoArgs += action;

        /// <summary>
        /// Removes an action with no arguments from the EventBinding
        /// </summary>
        /// <param name="action">Action with no arguments</param>
        public void Remove(Action action) => onEventNoArgs -= action;

        /// <summary>
        /// Adds an action with arguments to the EventBinding 
        /// </summary>
        /// <param name="action">Action with arguments</param>
        public void Add(Action<T> action) => onEvent += action;

        /// <summary>
        /// Removes an action with arguments from the EventBinding
        /// </summary>
        /// <param name="action">Action with arguments</param>

        public void Remove(Action<T> action) => onEvent -= action;
    }
}