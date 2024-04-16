using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using ExtensionMethods;
using UnityEngine;

public abstract class MonoBehaviourStateMachine<T> : MonoBehaviour, IStateMachine<T> where T : MonoBehaviourState<T>
{
    [SerializeField]
    protected Stack<T> _stateStack = new Stack<T>();
    public Stack<T> stateStack
    {
        get { return _stateStack; }
        protected set { _stateStack = value; }
    }
    public virtual T currentState { get; protected set; }
    public T previousState => stateStack.Peek();

    protected virtual void Update()
    {
        currentState?.UpdateExecution();
    }
    protected virtual void FixedUpdate()
    {
        currentState?.FixedUpdateExecution();
    }

    public virtual void ChangeToState(Type t)
    {
        if (!t.IsAssignableFrom(typeof(T))) throw new ArgumentException("Type " + t + " is not a subtype of " + typeof(T).Name);
        T newState = this.GetOrAddComponent<T>();
        ChangeToState(newState);
    }
    protected virtual void ChangeToState(T newState)
    {
        // null check
        if (newState == null) throw new ArgumentNullException("New State to substitute into is null!");

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
        if (newState == null) throw new ArgumentNullException("New State to substitute into is null!");
        if (previousState.Equals(newState))
        {
            ChangeToPreviousState();
            return;
        }
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    void IStateMachine<T>.ChangeToState(T newState)
    {
        throw new NotImplementedException();
    }
}
