using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class MonoBehaviourState<T> : MonoBehaviour, IState<T> where T : MonoBehaviourState<T>
{
    public abstract IStateMachine<T> stateMachine { get; }

    public abstract void Enter(IStateMachine<T> newStateMachine);
    public abstract void Execute();
    public abstract void Exit();
}
