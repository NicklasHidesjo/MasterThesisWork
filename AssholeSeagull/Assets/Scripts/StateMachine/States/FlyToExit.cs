using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToExit : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;


    Transform transform;
    Vector3 target;

    float speed;
    float acceleration;

    public FlyToExit (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        transform = seagullController.transform;

        speed = 0;
        acceleration = seagullController.SeagullSettings.acceleration * 2;

        target = seagullController.FlightEnd;

        transform.LookAt(target);
    }

    public void Execute()
    {
        MoveBird();
        Accelerate();

        if (ArrivedAtTarget())
		{
			DeactivateBird();
		}
	}

	private void MoveBird()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    private void Accelerate()
    {
        speed += acceleration * Time.fixedDeltaTime;
    }
	
    private void DeactivateBird()
	{
		FoodItem stolenFood = seagullController.StolenFood;

		if (stolenFood != null)
		{
			stolenFood.DeactivateFood();
		}

		transform.gameObject.SetActive(false);
	}

    private bool ArrivedAtTarget()
    {
        return transform.position == target;
    }

    public void Exit()
    {
        
    }
}
