using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToStealFood : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;

        if (seagullController.FoodTarget == null)
        {
            Debug.Log("No food found changing to poop");
            stateMachine.ChangeState(new FlyToPoop());
        }
        else
        {
            seagullController.SetFoodPos();
            seagullController.LookAt();
        }
    }

    public void Execute()
    {
        seagullController.MoveBird();
        seagullController.Deaccelerate();

        if (seagullController.IsScared)
        {
            stateMachine.ChangeState(new Flee());
        }
        if (seagullController.ArrivedAtTarget())
        {
            stateMachine.ChangeState(new StealFood());
        }
    }

    public void Exit()
    {
        seagullController.SetSpeed();
    }
}
