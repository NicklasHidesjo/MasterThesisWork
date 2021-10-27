using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPoop : IState
{
    SeagullController seagullController;
    StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        seagullController.SetPackagePos();
        seagullController.LookAt();
    }

    public void Execute()
    {
        seagullController.MoveBird();
        seagullController.Deaccelerate();

        if(seagullController.IsScared)
        {
            stateMachine.ChangeState(new Flee());
        }

        if (seagullController.ArrivedAtTarget())
        {
            stateMachine.ChangeState(new PoopState());
        }
    }

    public void Exit()
    {
        seagullController.SetSpeed();
    }
}
