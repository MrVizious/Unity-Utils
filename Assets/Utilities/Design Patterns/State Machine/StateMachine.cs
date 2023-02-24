using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public abstract class StateMachine<T> : MonoBehaviour where T : State<T>
    {
        protected Stack<T> stateStack;
        protected T currentState;

        public abstract void ChangeToState(T newState);
        public abstract void ChangeToPreviousState();
        public abstract void SubstituteStateWith(T newState);
    }
}
