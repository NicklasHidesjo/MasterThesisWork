using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToStealFood : IState
{
    // Make it fly to a certain elevation, fix so that it avoids trees and stuff
    // make seagull change state to a "divesteal" state 
    // that state should then go over to the steal food state.

    private SeagullController seagullController;
    private StateMachine stateMachine;

    Transform transform;
    Transform target;
    Vector3 offset;

    float minSpeed;
    float speed;
    float deacceleration;

    float distanceToFood;

    float minimumElevation;
    float diveDistance;
    bool diving;

    public FlyToStealFood (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        transform = seagullController.transform;

        offset = new Vector3(0, 0, 0);
    }

    public void Enter()
    {
        diving = false;

        SeagullController.Scared += Scared;

        minSpeed = seagullController.SeagullSettings.minSpeed;
        speed = seagullController.SeagullSettings.speed;
        deacceleration = seagullController.SeagullSettings.deacceleration;
        distanceToFood = seagullController.SeagullSettings.distanceToFood;
        minimumElevation = seagullController.SeagullSettings.YOffset;
        diveDistance = seagullController.SeagullSettings.diveDistance;


        target = seagullController.FoodTarget.transform;

        transform.LookAt(target);
    }

    public void Execute()
    {
        if(!diving)
        {
            MoveBird();
        }
        else
        {
            Dive();
        }
        Deaccelerate();

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= distanceToFood)
        {
            seagullController.SetFoodCollider = false;
        }

        if(distanceToTarget <= diveDistance)
        {
            diving = true;
        }


        if (ArrivedAtTarget())
        {
            stateMachine.ChangeState(States.StealFood);
        }
    }

    private void Dive()
    {
        // increase the speed to really show how he dives.
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.fixedDeltaTime);
    }

    private void MoveBird()
    {
        // make the bird maybe go to max elevation faster?
        Vector3 position = new Vector3(target.position.x, target.position.y + minimumElevation, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, position + offset, speed * Time.fixedDeltaTime);
    }

    private void Deaccelerate()
    {
        speed -= deacceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, Mathf.Infinity);
    }

    private bool ArrivedAtTarget()
    {
        return transform.position == target.position + offset;
    }


    private void Scared()
	{
        stateMachine.ChangeState(States.Flee);
    }
    
    public void Exit()
    {
        SeagullController.Scared -= Scared;
    }
}
