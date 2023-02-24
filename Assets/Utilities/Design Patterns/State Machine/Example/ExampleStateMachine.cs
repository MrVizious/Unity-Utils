using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ExampleStateMachine : StateMachine<ExampleState>
{
    public override void ChangeToPreviousState()
    {
        throw new System.NotImplementedException();
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
