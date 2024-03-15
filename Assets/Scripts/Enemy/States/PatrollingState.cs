using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrollingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private int currentPatrollingIndex = -1;
        private Vector3 destination;
        public PatrollingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter() 
        {
            SetNextWayPointIndex();
            destination = GetDestination();
            MoveTowardsDestination();
        }

        private void MoveTowardsDestination()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(destination);
        }

        private Vector3 GetDestination() => Owner.Data.PatrollingPoints[currentPatrollingIndex];

        private void SetNextWayPointIndex()
        {
            if (currentPatrollingIndex == Owner.Data.PatrollingPoints.Count - 1)
                currentPatrollingIndex = 0;
            else
                currentPatrollingIndex++;
        }

        public void Update() 
        {
            if (HasReachedDestination())
                stateMachine.ChangeState(States.IDLE);
        }

        public void OnStateExit() { }

        private bool HasReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
    }
}