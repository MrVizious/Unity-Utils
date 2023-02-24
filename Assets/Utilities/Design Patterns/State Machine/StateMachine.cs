using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// Usage: public class ExampleStateMachine : StateMachine<ExampleState> { ... }
    /// </summary>
    /// <typeparam name="T">State type</typeparam>
    public abstract class StateMachine<T> : MonoBehaviour where T : State
    {
        protected Stack<T> stateStack;
        protected T currentState;

        public abstract void ChangeToState(T newState);
        public abstract void ChangeToPreviousState();
        public abstract void SubstituteStateWith(T newState);
    }
}
