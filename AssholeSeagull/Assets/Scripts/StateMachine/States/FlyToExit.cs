using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToExit : IState
{
    SeagullController seagullController;
    StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
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
