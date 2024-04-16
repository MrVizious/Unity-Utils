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
        public Stack<T> stateStack { get; set; }
        [ShowInInspector]
        public T currentState { get; }
        public T previousState { get { return stateStack.Peek(); } }

        public void ChangeToState(Type t);
        public void ChangeToState(T newState);
        public void ChangeToPreviousState();
        public void SubstituteStateWith(T newState);

    }
}
