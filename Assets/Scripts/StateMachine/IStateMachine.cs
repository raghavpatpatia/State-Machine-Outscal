namespace StatePattern.StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState(States state);
    }

    public enum States
    {
        IDLE,
        ROTATING,
        SHOOTING,
        PATROLLING,
        CHASING
    }
}