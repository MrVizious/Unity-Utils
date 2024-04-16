using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class ExampleMonoBehaviourState : MonoBehaviourState<ExampleMonoBehaviourState>
{
    public override StateMachine<ExampleMonoBehaviourState> stateMachine => throw new System.NotImplementedException();

    public override void Enter(StateMachine<ExampleMonoBehaviourState> newStateMachine)
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
