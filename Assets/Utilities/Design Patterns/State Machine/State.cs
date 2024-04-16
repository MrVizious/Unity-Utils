using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// example	: '''public class MyClass : State<MyClass> { ... }'''
    /// </summary>
    public interface IState<T> where T : IState<T>
    {
        IStateMachine<T> stateMachine { get; }

        void Enter(IStateMachine<T> newStateMachine);
        void Exit();
    }
}
