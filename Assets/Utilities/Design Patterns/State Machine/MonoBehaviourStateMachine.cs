using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class MonoBehaviourStateMachine<T> : MonoBehaviour, IStateMachine<T> where T : MonoBehaviourState<T>
{
    public Stack<T> stateStack { get; set; }
    public virtual T currentState { get; protected set; }

    public abstract void ChangeToPreviousState();
    public abstract void ChangeToState(Type t);
    public abstract void ChangeToState(T newState);
    public abstract void SubstituteStateWith(T newState);
}
