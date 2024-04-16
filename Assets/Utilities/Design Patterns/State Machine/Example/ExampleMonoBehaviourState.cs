using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class ExampleMonoBehaviourState : MonoBehaviourState<ExampleMonoBehaviourState>
{
    public override IStateMachine<ExampleMonoBehaviourState> stateMachine => throw new System.NotImplementedException();

    public override void Enter(IStateMachine<ExampleMonoBehaviourState> newStateMachine)
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
