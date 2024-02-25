using System.Collections.Generic;

namespace DesignPatterns.EventBus
{

    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> bindings = new();

        /// <summary>
        /// Register an EventBinding of type T to the EventBus of the same type
        /// </summary>
        /// <param name="binding">EventBinding of type T that contains all the actions to handle when the event is raised</param>
        public static void Register(EventBinding<T> binding) => bindings.Add(binding);

        /// <summary>
        /// Register an EventBinding of type T to the EventBus of the same type
        /// </summary>
        /// <param name="binding">EventBinding of type T that contains all the actions to handle when the event is raised</param>
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        /// <summary>
        /// Raise an event, can contain the event as argument to pass data through it
        /// </summary>
        public static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
    }

}