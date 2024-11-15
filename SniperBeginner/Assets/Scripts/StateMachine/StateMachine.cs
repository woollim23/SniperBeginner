public class StateMachine
{
    public IState CurrentState { get; private set; }

    public virtual void ChangeState(IState newState)
    {
        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }

    public virtual void Update()
    {
        CurrentState?.Update();
    }
}