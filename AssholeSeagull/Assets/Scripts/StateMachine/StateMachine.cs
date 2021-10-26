using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private SeagullController seagullController;
    private IState currentState;

    private void Awake()
    {
        seagullController = GetComponent<SeagullController>();
    }

    private void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(seagullController, this);
    }
}
