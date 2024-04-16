using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMonoBehaviourStateMachine : MonoBehaviourStateMachine<ExampleMonoBehaviourState>
{
    public override Stack<ExampleMonoBehaviourState> stateStack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override ExampleMonoBehaviourState currentState => throw new NotImplementedException();

    public override void ChangeToPreviousState()
    {
        throw new NotImplementedException();
    }

    public override void ChangeToState(Type t)
    {
        throw new NotImplementedException();
    }

    public override void ChangeToState(ExampleMonoBehaviourState newState)
    {
        throw new NotImplementedException();
    }

    public override void SubstituteStateWith(ExampleMonoBehaviourState newState)
    {
        throw new NotImplementedException();
    }
}
