using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using System;

public class ExampleStateMachine : IStateMachine<ExampleState>
{
    public Stack<ExampleState> stateStack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ExampleState currentState => throw new NotImplementedException();

    public void ChangeToPreviousState()
    {
        throw new NotImplementedException();
    }

    public void ChangeToState(Type t)
    {
        throw new NotImplementedException();
    }

    public void ChangeToState(ExampleState newState)
    {
        throw new NotImplementedException();
    }

    public void SubstituteStateWith(ExampleState newState)
    {
        throw new NotImplementedException();
    }
}
