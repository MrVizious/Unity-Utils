using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DesignPatterns
{
    /// <summary>
    /// Usage: public class ExampleStateMachine : StateMachine<ExampleState> { ... }
    /// </summary>
    /// <typeparam name="T">State type</typeparam>
    public abstract class StateMachine<T> : MonoBehaviour where T : State<T>
    {
        [ShowInInspector]
        public Stack<T> stateStack { get; set; }
        [ShowInInspector]
        public T currentState { get; protected set; }
        public T previousState { get { return stateStack.Peek(); } }

        protected virtual void Awake()
        {
            stateStack = new Stack<T>();
        }
        public virtual void ChangeToState(T newState)
        {
            // null check
            if (newState == null) Debug.LogError("New State to change into is null!");

            // no current state
            if (currentState == null)
            {
                currentState = newState;
                currentState.Enter(this);
                return;
            }

            // trying to change into current state
            if (currentState.Equals(newState)) return;

            // previous state is same as new one
            if (stateStack.Count > 0 && previousState.Equals(newState))
            {
                currentState?.Exit();
                stateStack.Pop();
                stateStack.Push(currentState);
                currentState = newState;
                currentState.Enter(this);
                return;
            }

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
