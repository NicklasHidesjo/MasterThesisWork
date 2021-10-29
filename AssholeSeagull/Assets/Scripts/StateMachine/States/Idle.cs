using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public Idle (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        seagullController.PlaySpawnSound();
        
        int random = Random.Range(0, 2);

        //Remove this, only for testing
        random = 1;

        if (random == 0)
        {
            stateMachine.ChangeState(States.FlyToPoop);
        }
        else
        {
            stateMachine.ChangeState(States.FlyToStealFood);
        }
    }

    public void Execute()
    {

    }
    public void Exit()
    {

    }
}
