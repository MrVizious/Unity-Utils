using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using System;

public class ExampleStateMachine : IStateMachine<ExampleState>
{
    public Stack<ExampleState> stateStack => throw new NotImplementedException();

    public ExampleState currentState => throw new NotImplementedException();

    public ExampleState previousState => throw new NotImplementedException();

    public ExampleState ChangeToPreviousState()
    {
        throw new NotImplementedException();
    }

    public ExampleState ChangeToState(Type t)
    {
        throw new NotImplementedException();
    }

    public ExampleState ChangeToState(ExampleState newState)
    {
        throw new NotImplementedException();
    }

    public ExampleState PrepareState(Type t)
    {
        throw new NotImplementedException();
    }

    public ExampleState SubstituteStateWith(Type t)
    {
        throw new NotImplementedException();
    }

    public ExampleState SubstituteStateWith(ExampleState newState)
    {
        throw new NotImplementedException();
    }
}
