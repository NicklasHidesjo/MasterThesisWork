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
        seagullController.PlaySpawnSound();
        // Scared seagull animation
        stateMachine.ChangeState(new FlyToExit()); // Move to execute once we have scared ani
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
