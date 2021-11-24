using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealFood : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;


    public StealFood(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        // maybe have noisy irritating stealing food in your face sound.
        FoodItem foodTarget = seagullController.FoodTarget;
        Debug.Log("Stealing Food: " + foodTarget.gameObject.name);

        foodTarget.transform.parent = seagullController.transform;
        foodTarget.Stolen = true;
        foodTarget.GetComponent<Rigidbody>().isKinematic = true;
        seagullController.StolenFood = foodTarget;
        //grabbyFeet.SetFoodRB(foodTarget.GetComponent<Rigidbody>());

        stateMachine.ChangeState(States.FlyToExit);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}
