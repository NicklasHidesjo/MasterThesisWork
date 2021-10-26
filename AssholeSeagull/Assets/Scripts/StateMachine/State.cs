
public interface IState
{
    public void Enter(SeagullController seagullController, StateMachine stateMachine);
    public void Execute();
    public void Exit();
}

public class PoopOn : IState
{
    SeagullController seagullController;
    StateMachine stateMachine;

    public void Enter(SeagullController seagullController, StateMachine stateMachine)
    {
        this.seagullController = seagullController;
        this.stateMachine = stateMachine;
        seagullController.Init();
    }

    public void Execute()
    {
        seagullController.MoveBird();
    }

    public void Exit()
    {
        
    }
}
