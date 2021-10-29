using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToExit : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public FlyToExit (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        seagullController.SetExitPos();
        seagullController.LookAt();
    }

    public void Execute()
    {
        seagullController.MoveBird();
        seagullController.Accelerate();

        if (seagullController.ArrivedAtTarget())
        {
            seagullController.Deactivate();
        }
    }

    public void Exit()
    {

    }
}
