using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class MonoBehaviourState<T> : MonoBehaviour, State<T> where T : MonoBehaviourState<T>
{
    public abstract StateMachine<T> stateMachine { get; }

    public abstract void Enter(StateMachine<T> newStateMachine);
    public abstract void Execute();
    public abstract void Exit();
}
