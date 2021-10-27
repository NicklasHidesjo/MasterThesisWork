
public interface IState
{
    public void Enter(SeagullController seagullController, StateMachine stateMachine);
    public void Execute();
    public void Exit();
}
