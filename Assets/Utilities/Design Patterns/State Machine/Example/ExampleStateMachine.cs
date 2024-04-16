using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using System;

public class ExampleStateMachine : StateMachine<ExampleState>
{
    public override void ChangeToPreviousState()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeToState(Type t)
    {
        throw new NotImplementedException();
    }

    public override void ChangeToState(ExampleState newState)
    {
        throw new System.NotImplementedException();
    }

    public override void SubstituteStateWith(ExampleState newState)
    {
        throw new System.NotImplementedException();
    }

}
