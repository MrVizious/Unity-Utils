using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// example	: '''public class MyClass : State<MyClass> { ... }'''
    /// </summary>
    public abstract class State : MonoBehaviour
    {
        protected StateMachine<State> _stateMachine;
        public StateMachine<State> stateMachine
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

        public abstract void Enter();
        public abstract void Exit();
    }
}
