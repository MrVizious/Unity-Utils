using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// Usage: public class ExampleStateMachine : StateMachine<ExampleState> { ... }
    /// </summary>
    /// <typeparam name="T">State type</typeparam>
    public abstract class StateMachine<T> : MonoBehaviour where T : State<T>
    {
        public Stack<T> stateStack { get; protected set; }
        public T currentState { get; protected set; }

        public virtual void ChangeToState(T newState)
        {
            if (newState == null) Debug.LogError("New State to change into is null!");
            currentState?.Exit();
            stateStack.Push(currentState);
            currentState = newState;
            currentState.Enter(this);
        }
        public virtual void ChangeToPreviousState()
        {
            currentState?.Exit();
            currentState = stateStack.Pop();
            currentState?.Enter(this);
        }
        public virtual void SubstituteStateWith(T newState)
        {
            if (newState == null) Debug.LogError("New State to substitute into is null!");
            currentState?.Exit();
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
