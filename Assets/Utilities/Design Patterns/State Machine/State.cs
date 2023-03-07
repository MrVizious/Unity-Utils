using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// example	: '''public class MyClass : State<MyClass> { ... }'''
    /// </summary>
    public abstract class State<T> : MonoBehaviour where T : State<T>
    {
        protected StateMachine<T> _stateMachine;
        public StateMachine<T> stateMachine
        {
            get
            {
                return _stateMachine;
            }
            set
            {
                if (value == null) return;
                _stateMachine = value;
            }
        }

        public virtual void Enter(StateMachine<T> newStateMachine)
        {
            stateMachine = newStateMachine;
        }
        public virtual void Exit(StateMachine<T> newStateMachine) { }
    }
}
