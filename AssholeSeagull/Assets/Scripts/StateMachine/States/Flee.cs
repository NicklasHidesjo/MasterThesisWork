using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public Flee (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        seagullController.PlaySpawnSound();
        // Scared seagull animation
        stateMachine.ChangeState(States.FlyToExit); // Move to execute once we have scared ani
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
