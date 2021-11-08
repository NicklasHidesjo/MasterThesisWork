using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToPoop : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    Transform transform;
    Vector3 target;

    float minSpeed;
    float speed;
    float deacceleration;

    public FlyToPoop (SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        SeagullController.Scared += Scared;

        transform = seagullController.transform;

        minSpeed = seagullController.SeagullSettings.minSpeed;
        speed = seagullController.SeagullSettings.speed;
        deacceleration = seagullController.SeagullSettings.deacceleration;

        Vector3 foodPackage = seagullController.FoodPackage;
        Vector3 packagePos = new Vector3(foodPackage.x, transform.position.y, foodPackage.z);
        target = packagePos;

        transform.LookAt(target);
    }

    public void Execute()
    {
        MoveBird();
        Deaccelerate();

        if (ArrivedAtTarget())
        {
            stateMachine.ChangeState(States.PoopState);
        }
    }

    private void MoveBird()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    private void Deaccelerate()
    {
        speed -= deacceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, Mathf.Infinity);
    }

    private bool ArrivedAtTarget()
    {
        return transform.position == target;
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
