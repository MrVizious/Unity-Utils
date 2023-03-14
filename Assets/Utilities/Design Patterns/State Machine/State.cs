using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// example	: '''public class MyClass : State<MyClass> { ... }'''
    /// </summary>
    public interface State<T> where T : State<T>
    {
        StateMachine<T> stateMachine { get; }

        void Enter(StateMachine<T> newStateMachine);
        void Exit();
    }
}
