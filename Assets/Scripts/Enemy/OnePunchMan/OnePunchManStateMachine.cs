using StatePattern.Enemy;
using System.Collections.Generic;

public class OnePunchManStateMachine
{
    private OnePunchManController Owner;
    protected Dictionary<OnePunchManStates, IStates> States = new Dictionary<OnePunchManStates, IStates>();
    private IStates currentState;
    public OnePunchManStateMachine(OnePunchManController Owner)
    {
        this.Owner = Owner;
        CreateStates();
        SetOwner();
    }

    private void CreateStates()
    {
        States.Add(OnePunchManStates.IDLE, new IdleState(this));
        States.Add(OnePunchManStates.ROTATING, new RotatingState(this));
        States.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
    }

    private void SetOwner()
    {
        foreach (IStates state in States.Values)
        {
            state.Owner = Owner;
        }
    }

    public void Update() => currentState?.Update();

    protected void ChangeState(IStates newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(OnePunchManStates newState) => ChangeState(States[newState]);
}