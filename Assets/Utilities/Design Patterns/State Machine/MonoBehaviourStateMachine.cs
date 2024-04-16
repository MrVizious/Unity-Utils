using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class MonoBehaviourStateMachine<T> : MonoBehaviour, StateMachine<T> where T : MonoBehaviourState<T>
{
    public abstract Stack<T> stateStack { get; set; }
    public abstract T currentState { get; }

    public abstract void ChangeToPreviousState();
    public abstract void ChangeToState(Type t);
    public abstract void ChangeToState(T newState);
    public abstract void SubstituteStateWith(T newState);
}
