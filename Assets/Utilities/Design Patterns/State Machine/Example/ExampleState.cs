using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ExampleState : IState<ExampleState>
{
    public IStateMachine<ExampleState> stateMachine => throw new System.NotImplementedException();

    public void Enter(IStateMachine<ExampleState> newStateMachine)
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
