using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private SeagullController seagullController;
    private IState currentState;

    private Idle idle;
    private FlyToStealFood flyToStealFood;
    private StealFood stealFood;
    private FlyToPoop flyToPoop;
    private PoopState poopState;
    private FlyToExit flyToExit;
    private Flee flee;

    // make references here for all different states

    private void Awake()
    {
        seagullController = GetComponent<SeagullController>();

        idle = new Idle(seagullController, this);
        flyToStealFood = new FlyToStealFood(seagullController, this);
        stealFood = new StealFood(seagullController, this);
        flyToPoop = new FlyToPoop(seagullController, this);
        poopState = new PoopState(seagullController, this);
        flyToExit = new FlyToExit(seagullController, this);
        flee = new Flee(seagullController, this);
    }

    private void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(States state) // change to take a enum for state
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        switch (state)
        {
            case States.Idle:
                currentState = idle;
                break;
            case States.FlyToStealFood:
                currentState = flyToStealFood;
                break;
            case States.StealFood:
                currentState = stealFood;
                break;
            case States.FlyToPoop:
                currentState = flyToPoop;
                break;
            case States.PoopState:
                currentState = poopState;
                break;
            case States.FlyToExit:
                currentState = flyToExit;
                break;
            case States.Flee:
                currentState = flee;
                break;
            default: Debug.LogError(state + " not in switch");
                break;
        }

        Debug.Log(currentState);
        currentState.Enter();
    }
}
