using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ExampleState : State<ExampleState>
{
    public StateMachine<ExampleState> stateMachine => throw new System.NotImplementedException();

    public void Enter(StateMachine<ExampleState> newStateMachine)
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
