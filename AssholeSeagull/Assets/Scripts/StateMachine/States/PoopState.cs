using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : IState
{
    private SeagullController seagullController;
    private StateMachine stateMachine;

    private Animator animator;

    public PoopState(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        animator = seagullController.GetComponent<Animator>();
    }

    public void Enter()
    {
        animator.ResetTrigger("Poop");
        animator.ResetTrigger("FlyAway");

        animator.SetTrigger("Poop");
    }

    public void Execute()
    {
        // have event instead?
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pooping"))
        {
            return;
        }
        stateMachine.ChangeState(States.FlyToExit);
    }

    public void Exit()
    {

    }
}