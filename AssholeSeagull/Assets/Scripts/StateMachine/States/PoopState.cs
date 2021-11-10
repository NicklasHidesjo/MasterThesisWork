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

        Pooping.Pooped += DonePooping;
    }

    public void Execute()
    {

    }

    private void DonePooping(SeagullController seagull)
	{
        if(seagull == seagullController)
		{
            stateMachine.ChangeState(States.FlyToExit);
        }
	}

    public void Exit()
    {
        Pooping.Pooped -= DonePooping;
    }
}