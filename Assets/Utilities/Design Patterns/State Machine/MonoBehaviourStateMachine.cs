using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using ExtensionMethods;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DesignPatterns
{

    public abstract class MonoBehaviourStateMachine<T> : MonoBehaviour, IStateMachine<T> where T : MonoBehaviourState<T>
    {
        public bool debug = false;
        protected Stack<T> _stateStack = new Stack<T>();
        [ShowInInspector]
        public virtual Stack<T> stateStack
        {
            get { return _stateStack; }
            protected set { _stateStack = value; }
        }
        [ShowInInspector]
        public virtual T currentState
        {
            get;
            protected set;
        }
        [ShowInInspector]
        public virtual T previousState
        {
            get
            {
                if (stateStack == null || stateStack.Count <= 0) return null;
                return stateStack.Peek();
            }
        }

        protected virtual void Update()
        {
            currentState?.UpdateExecution();
        }
        protected virtual void FixedUpdate()
        {
            currentState?.FixedUpdateExecution();
        }

        [Button]
        public virtual T ChangeToState(string type)
        {
            return ChangeToState(Type.GetType(type));
        }
        [Button]
        public virtual T ChangeToState(Type t)
        {
            Debug.Log("Type is " + t.ToString());
            if (!typeof(T).IsAssignableFrom(t)) throw new ArgumentException("Type " + t + " is not a subtype of " + typeof(T).Name);
            T newState = PrepareState(t);
            return ChangeToState(newState);
        }
        [Button]
        public virtual T ChangeToState(T newState)
        {
            if (debug) Debug.Log($"Change To State {newState}", this);
            // null check
            if (newState == null) throw new ArgumentNullException("New State to change to is null!");

            Type type = newState.GetType();
            T existingInstance = (T)this.GetComponent(type);
            if (existingInstance != null && !existingInstance.Equals(newState)) Destroy(existingInstance);

            // no current state
            if (currentState == null)
            {
                currentState = newState;
                currentState.Enter(this);
                return currentState;
            }

            // trying to change into current state
            if (currentState.Equals(newState)) return currentState;

            // previous state is same as new one
            if (stateStack.Count > 0 && previousState.Equals(newState))
            {
                currentState?.Exit();
                stateStack.Pop();
                stateStack.Push(currentState);
                currentState = newState;
                currentState.Enter(this);
                return currentState;
            }

            currentState?.Exit();
            stateStack.Push(currentState);
            currentState = newState;
            currentState.Enter(this);
            return currentState;
        }

        [Button]
        public virtual T ChangeToPreviousState()
        {
            if (stateStack == null || stateStack.Count <= 0)
            {
                Debug.LogError("Can't go to previous state because it doesn't exist");
                return null;
            }
            if (debug) Debug.Log($"Change To Previous State", this);
            currentState?.Exit();
            currentState = stateStack.Pop();
            currentState?.Enter(this);
            return currentState;
        }

        [Button]
        public virtual T SubstituteStateWith(string type)
        {
            return SubstituteStateWith(Type.GetType(type));
        }
        [Button]
        public virtual T SubstituteStateWith(Type t)
        {
            if (!typeof(T).IsAssignableFrom(t)) throw new ArgumentException("Type " + t + " is not a subtype of " + typeof(T).Name);
            T newState = PrepareState(t);
            return SubstituteStateWith(newState);
        }
        [Button]
        public virtual T SubstituteStateWith(T newState)
        {
            if (debug) Debug.Log($"Substitute with State {newState}", this);
            if (newState == null) throw new ArgumentNullException("New State to substitute into is null!");
            if (newState.Equals(previousState))
            {
                return ChangeToPreviousState();
            }
            currentState?.Exit();
            currentState = newState;
            currentState.Enter(this);
            return currentState;
        }

        public T PrepareState(Type t)
        {
            if (!typeof(T).IsAssignableFrom(t)) throw new ArgumentException("Type " + t + " is not a subtype of " + typeof(T).Name);
            return (T)this.GetOrAddComponent(t);
        }
    }

}