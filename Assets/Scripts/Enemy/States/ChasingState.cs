using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class ChasingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private PlayerController target;
        public ChasingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() 
        {
            SetTarget();
            SetStopDistance();
        }

        public void Update() 
        {
            MoveTowardsTarget();
            if (HasReachedDestination())
            {
                ResetPath();
                stateMachine.ChangeState(States.SHOOTING);
            }
        }

        public void OnStateExit() { }

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();
        private void SetStopDistance() => Owner.Agent.stoppingDistance = Owner.Data.PlayerStoppingDistance;
        private void MoveTowardsTarget() => Owner.Agent.SetDestination(target.Position);
        private bool HasReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
        private void ResetPath()
        {
            Owner.Agent.isStopped = true;
            Owner.Agent.ResetPath();
        }
    }
}