using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController>
    {
        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(States.IDLE, new IdleState<OnePunchManController>(this));
            states.Add(States.ROTATING, new RotatingState<OnePunchManController>(this));
            states.Add(States.SHOOTING, new ShootingState<OnePunchManController>(this));
        }
    }
}