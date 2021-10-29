using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    public PoopState(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        seagullController.SetAnimation("Poop");
    }

    public void Execute()
    {
        if (seagullController.IsInAnimation("Pooping"))
        {
            return;
        }
        stateMachine.ChangeState(States.FlyToExit);
    }

    public void Exit()
    {

    }
}