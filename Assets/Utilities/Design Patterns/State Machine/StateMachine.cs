using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace DesignPatterns
{
    /// <summary>
    /// Usage: public class ExampleStateMachine : StateMachine<ExampleState> { ... }
    /// </summary>
    /// <typeparam name="T">State type</typeparam>
    public interface IStateMachine<T> where T : IState<T>
    {
        [ShowInInspector]
        public Stack<T> stateStack { get; }
        [ShowInInspector]
        public T currentState { get; }
        public T previousState { get; }

        public T PrepareState(Type t);
        public T ChangeToState(Type t);
        public T ChangeToState(T newState);
        public T ChangeToPreviousState();
        public T SubstituteStateWith(Type t);
        public T SubstituteStateWith(T newState);

    }
}
