using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        seagullController.PlaySpawnSound();
        
        int random = Random.Range(0, 2);

        //Remove this, only for testing
        random = 0;

        if (random == 0)
        {
            stateMachine.ChangeState(new FlyToPoop());
        }
        else
        {
            stateMachine.ChangeState(new FlyToStealFood());
        }
    }

    public void Execute()
    {

    }
    public void Exit()
    {

    }
}
