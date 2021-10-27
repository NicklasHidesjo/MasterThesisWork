using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : IState
{
    SeagullController seagullController;
    StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        seagullController.SetAnimation("Poop");
    }

    public void Execute()
    {
        if (seagullController.IsInAnimation("Pooping"))
        {
            return;
        }
        stateMachine.ChangeState(new FlyToExit());
    }

    public void Exit()
    {

    }
}