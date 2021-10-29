using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToStealFood : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;
    public FlyToStealFood (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        if (seagullController.FoodTarget == null)
        {
            Debug.Log("No food found changing to poop");
            stateMachine.ChangeState(States.FlyToPoop);
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
            stateMachine.ChangeState(States.Flee);
        }
        if (seagullController.ArrivedAtTarget())
        {
            stateMachine.ChangeState(States.FlyToStealFood);
        }
    }

    public void Exit()
    {
        seagullController.SetSpeed();
    }
}
