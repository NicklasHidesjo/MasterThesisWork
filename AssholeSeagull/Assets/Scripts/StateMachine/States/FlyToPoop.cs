using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPoop : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public FlyToPoop (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        seagullController.SetPackagePos();
        seagullController.LookAt();
    }

    public void Execute()
    {
        seagullController.MoveBird();
        seagullController.Deaccelerate();

        // change to events?
        if(seagullController.IsScared)
        {
            stateMachine.ChangeState(States.Flee);
        }

        if (seagullController.ArrivedAtTarget())
        {
            stateMachine.ChangeState(States.PoopState);
        }
    }

    public void Exit()
    {
        seagullController.SetSpeed();
    }
}
