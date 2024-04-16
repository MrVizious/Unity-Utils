using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class MonoBehaviourState<T> : MonoBehaviour, IState<T> where T : MonoBehaviourState<T>
{
    protected virtual MonoBehaviourStateMachine<T> _stateMachine { get; set; }
    public virtual IStateMachine<T> stateMachine
    {
        get { return _stateMachine; }
        protected set { _stateMachine = (MonoBehaviourStateMachine<T>)value; }
    }

    public virtual void Enter(IStateMachine<T> newStateMachine)
    {
        enabled = true;
        stateMachine = newStateMachine;
    }
    public virtual void Exit() { enabled = false; }
    public virtual void UpdateExecution() { }
    public virtual void FixedUpdateExecution() { }
}