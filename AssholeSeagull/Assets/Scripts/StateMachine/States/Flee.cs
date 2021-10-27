using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : IState
{
    SeagullController seagullController;
    StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
