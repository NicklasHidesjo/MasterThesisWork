using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToStealFood : IState
{
    // remember to do fancy stuff if you pick up a food item that
    // that seagulls target we need to abort this and shit on
    // a package instead or steal if from the hands
    // correctly

    private SeagullController seagullController;
    private StateMachine stateMachine;

    Transform transform;
    Transform target;
    Vector3 offset;

    float minSpeed;
    float speed;
    float deacceleration;

    public FlyToStealFood (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        transform = seagullController.transform;

        offset = new Vector3(0, 0, 0);
    }

    public void Enter()
    {
        SeagullController.Scared += Scared;

        if (seagullController.FoodTarget == null)
        {
            Debug.Log("No food found changing to poop");
            stateMachine.ChangeState(States.FlyToPoop);
        }
        else
        {

            minSpeed = seagullController.SeagullSettings.minSpeed;
            speed = seagullController.SeagullSettings.speed;
            deacceleration = seagullController.SeagullSettings.deacceleration;

            target = seagullController.FoodTarget.transform;

            transform.LookAt(target);
        }

    }

    public void Execute()
    {
        MoveBird();
        Deaccelerate();

        if (ArrivedAtTarget())
        {
            stateMachine.ChangeState(States.StealFood);
        }
    }

    public void MoveBird()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.fixedDeltaTime);
    }

    public void Deaccelerate()
    {
        speed -= deacceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, Mathf.Infinity);
    }

    public bool ArrivedAtTarget()
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
